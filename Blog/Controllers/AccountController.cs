using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    [Route("/Acc")]
    public class AccountController : Controller
    {   
        [Route("/news")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/profile")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
