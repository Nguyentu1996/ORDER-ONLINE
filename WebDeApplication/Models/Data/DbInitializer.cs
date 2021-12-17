using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Data
{
    public class DbInitializer
    {
        public static void SeedData(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            string user = "Admin";
            string password = "Admin123";
            if (_userManager.FindByNameAsync(user).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                _roleManager.CreateAsync(new IdentityRole("User")).Wait();

                var userIdentity = new IdentityUser
                {
                    UserName = "Admin",
                    Email = user,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                
                };

                var result = _userManager.CreateAsync(userIdentity, password).Result;
                if (result.Succeeded)
                {

                    //await _userManager.AddToRoleAsync(, "Admin");
                    string[] theArray = { "Admin", "User" };

                    _userManager.AddToRolesAsync(userIdentity, theArray).Wait();
                    //_userManager.AddToRoleAsync(userIdentity, User);

                }
            }
        }
    }
}
