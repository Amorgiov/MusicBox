using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicBox.Domain.Genres;
using MusicBox.Domain.UserCreateDto;
using MusicBox.Domain.UserUpdateDto;
using MusicBox.Domain.UserViewDto;

namespace MusicBox.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Domain.User.UserModel> _userManager;
        private readonly SignInManager<Domain.User.UserModel> _signInManager;
        private readonly IAllGenres _allGenres;
        
        public AccountController(UserManager<Domain.User.UserModel> userManager, SignInManager<Domain.User.UserModel> signInManager, IAllGenres allGenres)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _allGenres = allGenres;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new Domain.User.UserModel {Email = model.Email, UserName = model.Email};
            user.FavoriteTracks = new List<string>();
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null) => View(new LoginViewModel { ReturnUrl = returnUrl });
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var result = 
                await _signInManager.PasswordSignInAsync
                    (model.Email, model.Password, model.RememberMe, false);
            
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            
            return View(model);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            
            var model = new EditViewModel {Id = user.Id, Email = user.Email, UserName = user.UserName};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return View(model);
            user.Email = model.Email;
            user.UserName = model.UserName;
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return RedirectToAction("Index", "Home");
            
            foreach (var error in result.Errors)
            { 
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> EditPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            
            var model = new EditPasswordViewModel {Id = user.Id, Email = user.Email};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                var validator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<Domain.User.UserModel>)) as
                        IPasswordValidator<Domain.User.UserModel>;
                var hasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<Domain.User.UserModel>)) as IPasswordHasher<Domain.User.UserModel>;
                
                if (validator == null) return View(model);
                
                var result =
                    await validator.ValidateAsync(_userManager, user, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    if (hasher != null) user.PasswordHash = hasher.HashPassword(user, model.NewPassword);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }

            return View(model);
        }
        
        public IActionResult EditGenres()
        {
            var genres = _allGenres.AllGenreses;
            return View(genres);
        }
    }
}