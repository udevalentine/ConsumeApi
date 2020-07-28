using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientCall.Controllers
{
    [Authorize(Policy = "MustBeAdmin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            //this is for accessing the value you stored in the claims on successful login
            string name = User.FindFirstValue(ClaimTypes.Name);
            string email = User.FindFirstValue(ClaimTypes.Email);
            string roleName = User.FindFirstValue(ClaimTypes.Role);
            // you can use any of this values to query your enpoint if you need any record that the value can fetch for you
            return View();
        }
    }
}