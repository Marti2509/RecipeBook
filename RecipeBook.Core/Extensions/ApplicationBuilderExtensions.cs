using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Data.Models;
using static RecipeBook.Common.ApplicationConstants;

namespace RecipeBook.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app, string email)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole<Guid>> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminName))
                {
                    return;
                }

                IdentityRole<Guid> role =
                    new IdentityRole<Guid>(AdminName);

                await roleManager.CreateAsync(role);

                ApplicationUser adminUser =
                    await userManager.FindByEmailAsync(email);

                await userManager.AddToRoleAsync(adminUser, AdminName);
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
