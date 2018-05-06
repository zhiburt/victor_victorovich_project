using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Blog.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Blog.ViewModel.Authefication;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers
{
    public class LanguageController : Controller
    {

        public LanguageController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}