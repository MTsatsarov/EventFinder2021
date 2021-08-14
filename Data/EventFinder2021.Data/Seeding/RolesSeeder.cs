namespace EventFinder2021.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EventFinder2021.Common;
    using EventFinder2021.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);

            await SeedUserAsync(roleManager, GlobalConstants.AdministratorRoleName, userManager);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedUserAsync(RoleManager<ApplicationRole> roleManager, string roleName, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync("AdminEventFinder");
            if (user == null)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }

                var createdUser = new ApplicationUser()
                {
                    UserName = "AdminEventFinder",
                    PasswordHash = "Admin123",
                    Email = "AdminEventFinder@gmail.com",
                };
                await userManager.CreateAsync(createdUser);
                await userManager.AddToRoleAsync(createdUser, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
