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

            //var adminProfile = new Profile
            //{
            //    UserId = adminUser.Id
            //};

            ////creating a Moderator who can review report messages
            //var modUser = new ApplicationUser
            //{
            //    FirstName = Configuration.GetSection("AppSettings")["ModUser:FirstName"],
            //    UserName = Configuration.GetSection("AppSettings")["ModUser:UserName"],
            //    Email = Configuration.GetSection("AppSettings")["ModUser:UserEmail"]
            //};

            //var modProfile = new Profile
            //{
            //    UserId = modUser.Id
            //};

            string adminUserPassword = Configuration.GetSection("AppSettings")["AdminUser:UserPassword"];
            var adminUserCheck = await UserManager.FindByEmailAsync(Configuration.GetSection("AppSettings")["AdminUser:UserEmail"]);

            //string modUserPassword = Configuration.GetSection("AppSettings")["ModUser:UserPassword"];
            //var modUserCheck = await UserManager.FindByEmailAsync(Configuration.GetSection("AppSettings")["ModUser:UserEmail"]);

            if (adminUserCheck == null)
            {
                var createPowerUser = await UserManager.CreateAsync(adminUser, adminUserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(adminUser, "Admin");

                    //here we create Profile for the user
                    //userContext.Add(adminProfile);
                    //await userContext.SaveChangesAsync();
                }
            }

            //if (modUserCheck == null)
            //{
            //    var modUserCreate = await UserManager.CreateAsync(adminUser, adminUserPassword);
            //    if (modUserCreate.Succeeded)
            //    {
            //        //here we tie the new user to the "Moderator" role 
            //        await UserManager.AddToRoleAsync(modUser, "Moderator");

            //        //here we create Profile for the user
            //        userContext.Add(modProfile);
            //        await userContext.SaveChangesAsync();
            //    }
            //}
        }
    }
}
