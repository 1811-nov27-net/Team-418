using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    // Using ControllerBase instead of Controller since 
    //  we don't need a view here.
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SignInManager<IdentityUser> _signInManager { get; }

        public AccountController(SignInManager<IdentityUser> signInManager, IdentityDbContext db)
        {
            // Create the authentication server if it does not
            //  exist.
            db.Database.EnsureCreated();
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> Login(AccountViewModel account)
        {
            // Using the authorization database, attempt to sign in using signinmanager
            // Persist = true will set the cookie to keep the user logged in for 
            //  however long startup set the expiretimespan option for
            var result = await _signInManager.PasswordSignInAsync(account.UserName, account.Password,
                isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return StatusCode(403); // Forbidden, invalid login
            }

            // User was logged in, nothing to return
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Register(
           AccountViewModel account,
           [FromServices] UserManager<IdentityUser> userManager,
           [FromServices] RoleManager<IdentityRole> roleManager,
           bool isAdmin = false)
        {
            if (ModelState.IsValid)
            {
                // Creating the new user 
                var user = new IdentityUser(account.UserName);
                // Wait for database to be updated
                var userCreationResult = await userManager.CreateAsync(user, account.Password);

                if (!userCreationResult.Succeeded)
                {
                    if (userCreationResult.Errors.Count() <= 0)
                        return BadRequest();

                    switch(userCreationResult.Errors.First().Code)
                    {
                        // Invalid user name
                        case "InvalidUserName":
                        case "DuplicateUserName":
                            {
                                return StatusCode((int)HttpStatusCode.Conflict);
                            }
                        // Invalid password
                        case "PasswordTooShort":
                        case "PasswordRequiresNonAlphanumeric":
                        case "PasswordRequiresDigit":
                        case "PasswordRequiresLower":
                        case "PasswordRequresUpper":
                            {
                                return StatusCode((int)HttpStatusCode.Forbidden);
                            }
                        default:
                            {
                                return BadRequest();
                            }
                    }
                }

                // Set up user roles
                if (isAdmin)
                {
                    // Create the admin role if it does not exist
                    bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
                    if (!adminRoleExists)
                    {
                        var adminRole = new IdentityRole("Admin");
                        var adminCreationResult = await roleManager.CreateAsync(adminRole);
                        if (!adminCreationResult.Succeeded)
                        {
                            // Server error
                            return StatusCode(500, adminCreationResult);
                        }

                    }

                    // Add the admin role to the user
                    var userRoleSetResult = await userManager.AddToRoleAsync(user, "Admin");
                    if (!userRoleSetResult.Succeeded)
                    {
                        // Server error
                        return StatusCode(500, userRoleSetResult);
                    }
                }

                // Sign the newly created user in
                await _signInManager.SignInAsync(user, true);
                return NoContent();

            }
            // Model state is invalid
            else
            {
                // Generic client error 400
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpGet]
        public string LoggedInUser()
        {
            return User.Identity.Name;
        }
    }
}