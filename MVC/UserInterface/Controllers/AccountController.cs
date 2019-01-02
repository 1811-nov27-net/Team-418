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

        // TODO: Replace with Login2 once I have REST services
        [HttpPost]
        public ActionResult Login(AccountViewModel accountForm)
        {
            UserViewModel user = UserViewModel.GetByName(accountForm.UserName);
            if(user == null)
            {
                ModelState.AddModelError("Password", "Incorrect username or password");
                return View();
            }

            UserViewModel.CurrentUser = user;

            return RedirectToAction("Song", "SongIndex");
        }


        [HttpPost]
        public async Task<ActionResult> Login2(AccountViewModel formAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                // Get the user from the music database
                HttpRequestMessage musicUserRequest = CreateRequestToService(HttpMethod.Post, "api/");

                // Create the message to send to Account REST service
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/Account/Login", formAccount);
                // Send the request
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        ModelState.AddModelError("Password", "Incorrect username or password");
                    }
                    // Return to same view, for a new log in attempt

                    // TODO: Display error message for user
                    return View();
                }

                var success = PassCookiesToClient(response);
                if (!success)
                {
                    return View("Error");
                }

                // successful login
                return RedirectToAction("Index", "Library");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
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
        public ActionResult CreateNewAccount(AccountViewModel formAccount)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View();
                }

                UserViewModel user = new UserViewModel
                {
                    Name = formAccount.UserName,
                    Admin = formAccount.Admin
                };
                UserViewModel.CurrentUser = user;

                return RedirectToAction("SongIndex", "Song");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewAccount2(AccountViewModel formAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                // Use the Account/Register Post from the services to create a new user
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/Account/Register", formAccount);
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    switch(response.StatusCode)
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
                    return View("Error");
                }

                return RedirectToAction("Index", "Library");
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