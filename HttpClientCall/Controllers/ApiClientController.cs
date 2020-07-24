using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientCall.Infrastructure;
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
    }
}