using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Learning.Core.Domains.Identity;

namespace WPF_Learning.Model
{
    public class ApplicationContextSeed
    {
        public async Task SeedAsync(ApplicationContext context)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var rootAdminRoleId = Guid.NewGuid();
            var userAdminId = Guid.NewGuid();
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new AppRole()
                {
                    Id = rootAdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "Quản trị"
                });

                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var userAdmin = new AppUser()
                {
                    Id = userAdminId,
                    FirstName = "Huy",
                    LastName = "Trần",
                    Email = "huytq@ics-p.vn",
                    NormalizedEmail = "HUYTQ@ICS-P.VN",
                    UserName = "huytq",
                    NormalizedUserName = "HUYTQ",
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    DateCreated = DateTime.UtcNow,
                };
                userAdmin.PasswordHash = passwordHasher.HashPassword(userAdmin, "Admin@123$");
                await context.Users.AddAsync(userAdmin);
                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
                {
                    RoleId = rootAdminRoleId,
                    UserId = userAdminId,
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
