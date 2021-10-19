using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicBox.Domain.User;

namespace MusicBox.Infrastructure.Contexts
{
    public sealed class ApplicationContext : IdentityDbContext<UserModel>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}