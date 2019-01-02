using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class AccountController : AServiceController
    {
        public AccountController(HttpClient client) : base(client)
        {
        }

        // Go to login view 
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Login(AccountViewModel accountForm)
        {
            // Check for the user in the database
            await UserViewModel.SyncUsersAsync(Client);
            UserViewModel user = UserViewModel.GetByName(accountForm.UserName);

            if(user == null)
            {
                ModelState.AddModelError("Password", "Incorrect username or password");
                return View();
            }

            /*
            // User exists in the database, attempt to authorize
            // Create the message to send to Account REST service
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/Account/Login", accountForm);
            // Send the request
            HttpResponseMessage response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    ModelState.AddModelError("Password", "Incorrect username or password");
                }
                // Login unsuccessfull
                // Return to same view, for a new log in attempt
                return View();
            }

            var success = PassCookiesToClient(response);
            if (!success)
            {
                return View("Error");
            }
            */

            // Login was successful
            UserViewModel.CurrentUser = user;
            return RedirectToAction("SongIndex", "Song");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            UserViewModel.CurrentUser = null;
            return RedirectToAction("SongIndex", "Song");
        }

        [HttpGet]
        public async Task<ActionResult> Logout2()
        {
            try
            {
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Get, "api/Account/Logout");
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    // TODO: Error handling
                    return View();
                }

                bool success = PassCookiesToClient(response);
                if (!success)
                {
                    // TODO: Error handling
                    return View("Error");
                }

                return RedirectToAction("Index", "Library");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult CreateNewAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewAccount(AccountViewModel formAccount)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View();
                }

                UserViewModel cUser = UserViewModel.CurrentUser;

                // Check for the user in the database
                await UserViewModel.SyncUsersAsync(Client);

                // Ensure the user does not already exist
                if (UserViewModel.GetByName(formAccount.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "Username already exists!");
                    return View();
                }

                /* Authorization is down. Fix later
                // Username not taken, attempt to create user via authorization
                // Use the Account/Register Post from the services to create a new user
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/Account/Register", formAccount);
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    switch (response.StatusCode)
                    {
                        // Using forbidden as password unacceptable
                        case HttpStatusCode.Forbidden:
                            {
                                ModelState.AddModelError("Password", "Password does not meet required criteria.");
                                break;
                            }
                        // Using conflict as username already exists
                        case HttpStatusCode.Conflict:
                            {
                                ModelState.AddModelError("UserName", "User name taken.");
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    return View();
                }

                bool success = PassCookiesToClient(response);
                if (!success)
                {
                    return View();
                }
                */

                // User successfully created in Authorization database
                // Add user to MusicDB
                UserViewModel user = new UserViewModel
                {
                    Name = formAccount.UserName,
                    Admin = formAccount.Admin
                };

                HttpRequestMessage addUserRequest = CreateRequestToService(HttpMethod.Post, "api/user", new { name = user.Name, admin = user.Admin });
                HttpResponseMessage addUserResponse = await Client.SendAsync(addUserRequest);

                if (!addUserResponse.IsSuccessStatusCode)
                {
                    // Unable to add the user to the database...
                    // Return to same view, for a new log in attempt
                    return View();
                }

                // User was successfully created and added to both Authorization and
                //  Music databases.
                UserViewModel.CurrentUser = user;

                return RedirectToAction("SongIndex", "Song");
            }
            catch (Exception)
            {
                return View();
            }
        }
        
        // Take the cookie from the service REST response and add it to the 
        //  header of the response we are currently using
        private bool PassCookiesToClient(HttpResponseMessage apiResponse)
        {
            if (apiResponse.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                // here the "value" contains both the name and the value of the cookie
                var authValue = values.FirstOrDefault(x => x.StartsWith(cookieName));
                if (authValue != null)
                {
                    Response.Headers.Add("Set-Cookie", authValue);
                    return true;
                }
            }
            return false;
        }
    }
}