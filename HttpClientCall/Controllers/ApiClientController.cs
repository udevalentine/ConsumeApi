using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HttpClientCall.Infrastructure;
using HttpClientCall.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HttpClientCall.Controllers
{
    public class ApiClientController : Controller
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public ApiClientController(IConfiguration configuration)
        {
            _configuration = configuration;
            ApplicationSettings.ApiUrl= _configuration.GetValue<string>("BaseUrl:root");
        }
        public async Task<IActionResult> Index()
        {

            var data=await ApiApplicationFactory.newInstance.GetCountries();
            return View(data);
        }
        public async Task<IActionResult> Login(string username, string password)
        {
            
            UserLogin model = new UserLogin()
            {
                username = username,
                password = password,
            };
            //this calls an external endpoint to authenticate the user login detail
            //if authentications goes through it will return userModel 
            // please take note i have added authorization service on the startup class
            var userModel = await ApiApplicationFactory.newInstance.AuthenticateUser(model);
            if (userModel != null)
            {
                var identity = new ClaimsIdentity(new[] {
                         new Claim(ClaimTypes.Name, userModel.FullName),
                         new Claim(ClaimTypes.Email,userModel.Email),
                         new Claim(ClaimTypes.Role, userModel.Role),
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                if (userModel.Role == "Admin")
                {
                    //this redirects the logged in user to the controller he is authorized to access the resources. Admin controller
                    return RedirectToAction("Index", "Admin");
                }
                else if (userModel.Role == "User")
                {
                    //this redirects the logged in user to the controller he is authorized to access the resources. User controller
                    return RedirectToAction("Index", "User");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            //At this point the cookies is cleared
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }

    }
}