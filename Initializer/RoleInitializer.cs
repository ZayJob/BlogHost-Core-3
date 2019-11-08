using BlogHost.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogHost.Initializer
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Admin
            string adminEmail = "admin@admin.ru";
            string adminPassword = "admin123";

            //Moder
            string moderEmail = "moder@moder.ru";
            string moderPassword = "moder123";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("moder") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("moder"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true};
                IdentityResult resultAdmin = await userManager.CreateAsync(admin, adminPassword);

                if (resultAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            if (await userManager.FindByNameAsync(moderEmail) == null)
            {
                User moder = new User { Email = moderEmail, UserName = moderEmail, EmailConfirmed = true };
                IdentityResult resultModer = await userManager.CreateAsync(moder, moderPassword);

                if (resultModer.Succeeded)
                {
                    await userManager.AddToRoleAsync(moder, "moder");
                }
            }
        }
    }
}
