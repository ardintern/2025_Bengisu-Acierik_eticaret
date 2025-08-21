using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceWebSite.Identity
{
    public static class IdentityInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Roller
            if (!await roleManager.RoleExistsAsync("admin"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "admin", Description = "admin rolü" });

            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "user", Description = "user rolü" });

            // Kullanıcı 1
            var u1 = await userManager.FindByNameAsync("sadikturan");
            if (u1 == null)
            {
                u1 = new ApplicationUser
                {
                    Name = "sadık",
                    Surname = "turan",
                    UserName = "sadikturan",
                    Email = "sadikturan@gmail.com",
                    EmailConfirmed = true
                };
                var r1 = await userManager.CreateAsync(u1, "1234567");
                if (r1.Succeeded)
                {
                    await userManager.AddToRoleAsync(u1, "admin");
                    await userManager.AddToRoleAsync(u1, "user");
                }
            }

            // Kullanıcı 2
            var u2 = await userManager.FindByNameAsync("cinarturan");
            if (u2 == null)
            {
                u2 = new ApplicationUser
                {
                    Name = "Çınar",
                    Surname = "turan",
                    UserName = "cinarturan",
                    Email = "cinarturan@gmail.com",
                    EmailConfirmed = true
                };
                var r2 = await userManager.CreateAsync(u2, "1234567");
                if (r2.Succeeded)
                    await userManager.AddToRoleAsync(u2, "user");
            }
        }
    }
}
