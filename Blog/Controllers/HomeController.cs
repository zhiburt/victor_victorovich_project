using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Blog.Models.DbModels;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<UserIndentity> _signInManager;

        public HomeController(
            SignInManager<UserIndentity> signInManager
        )
        {
            this._signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if(_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Price()
        {

            return View();
        }

        public IActionResult Features()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
