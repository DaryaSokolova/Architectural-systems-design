using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index() //input
        {
            return View();
        }

        //public IActionResult Index() //input
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Privacy(string name, string password)//registration
        {
            Entity_new.Authorization obj = new Entity_new.Authorization();
            obj.Name = name;
            obj.Password = password;
            new BL.AuthorizationBL().Registration(obj);
            return View();
        }

        //public IActionResult Privacy(string name, string password)//registration
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
