using Incognito.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Incognito.Data
{
    public class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding customs roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var UserContext = serviceProvider.GetRequiredService<UserContext>();
            

            string[] roleNames = { "Admin", "Member", "Moderator" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //creating a Admin who could maintain the web app
            var adminUser = new ApplicationUser
            {
                FirstName = Configuration.GetSection("AppSettings")["AdminUser:FirstName"],
                UserName = Configuration.GetSection("AppSettings")["AdminUser:UserName"],
                Email = Configuration.GetSection("AppSettings")["AdminUser:UserEmail"]
            };

            string adminUserPassword = Configuration.GetSection("AppSettings")["AdminUser:UserPassword"];
            var adminUserCheck = await UserManager.FindByEmailAsync(Configuration.GetSection("AppSettings")["AdminUser:UserEmail"]);

            if (adminUserCheck == null)
            {
                var createPowerUser = await UserManager.CreateAsync(adminUser, adminUserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(adminUser, "Admin");

                    var adminProfile = new UserProfile
                    {
                        UserId = adminUser.Id
                    };

                    //here we create Profile for the user
                    UserContext.Add(adminProfile);
                    await UserContext.SaveChangesAsync();
                }
            }
        }
    }
}
