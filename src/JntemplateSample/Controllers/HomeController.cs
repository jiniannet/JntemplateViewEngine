using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JntemplateSample.Models;

namespace JntemplateSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.name = "jntemplate";
            ViewBag.Site = new {
                Name = "演示站点",
                Url = "http://www.jiniannet.com"
            };
            return View("Views/home/default.html");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
