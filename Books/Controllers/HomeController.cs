using System;
using System.Diagnostics;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Exception to test logging
            //throw new DivideByZeroException("Division by zero");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
