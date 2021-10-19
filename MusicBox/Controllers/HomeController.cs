using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MusicBox.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Domain.User.UserModel> _userManager;

        public HomeController(UserManager<Domain.User.UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Like(string trackId)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            
            var name = User?.Identity?.Name;
            var user = await _userManager.FindByNameAsync(name);
            
            if (user.FavoriteTracks.Contains(trackId) == false) user.FavoriteTracks.Add(trackId);
            else return RedirectToAction("Index");
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return RedirectToAction("Index", "Home");
            
            foreach (var error in result.Errors)
            { 
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(string trackId)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            
            var name = User?.Identity?.Name;
            var user = await _userManager.FindByNameAsync(name);

            if (user.FavoriteTracks.Contains(trackId)) user.FavoriteTracks.Remove(trackId);
            else RedirectToAction("Index");
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return RedirectToAction("Index");
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("Index");
        }
    }
}
