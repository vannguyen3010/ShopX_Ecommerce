using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Shared;
using System.Data;

namespace Ecommerce_Wolmart.API.Extensions
{
    public static class SeedData
    {
        public static async Task SeedAync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();

            // Kiểm tra xem các roles đã được tạo chưa
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new UserRole { Name = RoleEnum.SuperAdmin.ToString(), NormalizedName = RoleEnum.SuperAdmin.ToString().ToUpper() });
                await roleManager.CreateAsync(new UserRole { Name = RoleEnum.Admin.ToString(), NormalizedName = RoleEnum.Admin.ToString().ToUpper() });
                await roleManager.CreateAsync(new UserRole { Name = RoleEnum.User.ToString(), NormalizedName = RoleEnum.User.ToString().ToUpper() });
            }

            // Tạo tài khoản SuperAdmin nếu chưa tồn tại
            var superAdmin = await userManager.FindByEmailAsync("superadmin@example.com");
            if (superAdmin == null)
            {
                superAdmin = new User
                {
                    UserName = "superadmin@example.com",
                    Email = "superadmin@example.com",
                    //FirstName = "Super",
                    //LastName = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(superAdmin, "SuperAdmin.123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdmin, RoleEnum.SuperAdmin.ToString()); // Gán quyền SuperAdmin cho tài khoản này
                }
            }
        }
    }
}
