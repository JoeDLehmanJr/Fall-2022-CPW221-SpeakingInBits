using Microsoft.AspNetCore.Identity;
#nullable disable
namespace SpeakingInBits.Models
{
    public static class IdentityHelper
    {
        public const string Instructor = "Instructor";
        public const string Student = "Student";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();

            // If no users are present, make a default user
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count;
            if (numUsers == 0) // If no users are in the specified role
            {
                var defaultUser = new IdentityUser()
                {
                    Email = "instructor@speakinginbits.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(defaultUser, "my_Password3");

                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
