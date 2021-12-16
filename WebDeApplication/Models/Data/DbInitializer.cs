using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Data
{
    public class DbInitializer
    {

        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<IdentityUser> _userManager;
        //public DbInitializer(

        //    UserManager<IdentityUser> userManager,
        //    RoleManager<IdentityRole> roleManager)
        //{

        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}
        public static void SeedData(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            string user = "admin@gmail.com";
            string password = "Admin123";
            if (_userManager.FindByNameAsync(user).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole("Admin"));
                //_roleManager.CreateAsync(new IdentityRole("User"));

                var userIdentity = new IdentityUser
                {
                    UserName = user,
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

                    _userManager.AddToRoleAsync(userIdentity, "Admin");
                    //_userManager.AddToRoleAsync(userIdentity, User);

                }
            }
            //This example just creates an Administrator role and one Admin users
            //public async void Initialize()
            //{

            //    }
            //    ////create database schema if none exists
            //    ////_context.Database.EnsureCreated();

            //    ////If there is already an Administrator role, abort
            //    //if (_context.Roles.Any(r => r.Name == "Admin")) return;

            //    ////Create the Administartor Role
            //    //await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //    //await _roleManager.CreateAsync(new IdentityRole("User"));

            //    ////Create the default Admin account and apply the Administrator role
            //    //string user = "admin@gmail.com";
            //    //string password = "Admin123";
            //    //var userIdentity = new IdentityUser { UserName = user, Email = user };
            //    //var result = await _userManager.CreateAsync(userIdentity, password);

            //    ////await _userManager.CreateAsync(new IdentityUser { UserName = user, Email = user, EmailConfirmed = true }, password);
            //    //await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "Admin");
            //    //await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "User");
            ////}
        }
    }
}
