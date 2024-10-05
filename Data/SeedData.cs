using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using QQVault.Models;
using System;
using System.Threading.Tasks;

namespace GuitarShop.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            CreateUsers(app).Wait();
        }

        public static async Task CreateUsers(IApplicationBuilder app)
        {
            UserManager<User> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<User>>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync("Client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Client"));
            }

            // Admin
            await CreateUser(userManager, roleManager, new User
            {
                UserName = "Admin",
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "User",
                CellPhoneNumber = "1234567890",
                CreatedAt = DateTime.Now,
                UserType = UserType.Staff,
                NumberInput = "1234567"
            }, "AdminPassword123!", "Administrator");

            // Consultant
            await CreateUser(userManager, roleManager, new User
            {
                UserName = "Consultant",
                Email = "consultant@example.com",
                FirstName = "Consultant",
                LastName = "User",
                CellPhoneNumber = "2345678901",
                CreatedAt = DateTime.Now,
                UserType = UserType.Staff,
                NumberInput = "2345678"
            }, "ConsultantPassword123!", "Consultant");

            // Financial Adviser
            await CreateUser(userManager, roleManager, new User
            {
                UserName = "Financial Adviser",
                Email = "adviser@example.com",
                FirstName = "Financial",
                LastName = "Adviser",
                CellPhoneNumber = "3456789012",
                CreatedAt = DateTime.Now,
                UserType = UserType.Staff,
                NumberInput = "3456789"
            }, "AdviserPassword123!", "FinancialAdviser");

        }

        private static async Task CreateUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, User user, string password, string role)
        {
            if (await userManager.FindByNameAsync(user.UserName) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
