using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MusicBox.Domain.User
{
    public class UserModel : IdentityUser
    {
        public override string Id { get; set; }
        public override string Email { get; set; }
        public List<string> FavoriteTracks { get; set; }
    }
}