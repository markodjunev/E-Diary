namespace EDiary.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = new ApplicationUser
            {
                Email = "admin@abv.bg",
                UserName = "admin",
            };

            await SeedUserAsync(userManager, adminUser);

            await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var userExist = await userManager.FindByNameAsync(user.UserName);
            if (userExist == null)
            {
                var result = await userManager.CreateAsync(user, "123456");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
