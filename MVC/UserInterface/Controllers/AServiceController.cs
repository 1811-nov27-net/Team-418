using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserInterface.Controllers
{
    public class AServiceController : Controller
    {
        // This name should match the name of the cookie that 
        //  the REST service sets when a user logs in
        public static readonly string cookieName = "UserAuthentication";
        private static readonly Uri serviceUri = new Uri("https://project1-5rest.azurewebsites.net");

        public HttpClient Client { get; set; }

        public AServiceController(HttpClient client)
        {
            Client = client;
        }

        // Manually construct the HttpRequestMessage in order to
        //  modify the header with the client's authentication cookie
        public HttpRequestMessage CreateRequestToService(HttpMethod restMethodName, string uri, object body = null)
        {
            var apiRequest = new HttpRequestMessage(restMethodName, new Uri(serviceUri, uri));

            // Set object data in the request message if 
            //  there is anything to set
            if(body != null)
            {
                var jsonString = JsonConvert.SerializeObject(body);
                apiRequest.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            // Grab the application authorization cookie from
            //  the clients request. Will be null if they are not 
            //  already logged in.
            var cookieValue = Request.Cookies[cookieName];

            if(cookieValue != null)
            {
                // Add the cookie to the header of the message
                apiRequest.Headers.Add("Cookie", new CookieHeaderValue(cookieName, cookieValue).ToString());
            }

            return apiRequest;
               
        }

    }
}