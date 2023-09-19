using Application.enums;
using Infraestructure.CustomEntities;
using Microsoft.AspNetCore.Identity;

namespace Infraestructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed 
            var defaultUser = new ApplicationUser
            {
                UserName = "userBasic",
                Email = "userBasic@gmail.com",
                Nombre = "Pedro",
                Apellido = "Rivera",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Abc_12345");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
