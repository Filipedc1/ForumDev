using ForumDev.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDev.Data
{
    public class DataSeeder
    {
        private ApplicationDbContext dbContext;

        public DataSeeder(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public Task SeedSuperUser()
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            var userStore = new UserStore<ApplicationUser>(dbContext);

            var user = new ApplicationUser
            {
                UserName = "Admin",
                NormalizedUserName = "admin",
                Email = "forumdevsuper@gmail.com",
                NormalizedEmail = "forumdevsuper@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = hasher.HashPassword(user, "admin");
            user.PasswordHash = hashedPassword;

            var hasAdminRole = dbContext.Roles.Any(roles => roles.Name == "Admin");
            if (!hasAdminRole)
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "admin"
                });
            }

            var hasSuperUser = dbContext.Users.Any(u => u.NormalizedUserName == user.NormalizedUserName);
            if (!hasSuperUser)
            {
                userStore.CreateAsync(user);
                userStore.AddToRoleAsync(user, "Admin");
            }

            dbContext.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}
