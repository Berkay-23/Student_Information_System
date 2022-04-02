using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SISAPI.API.Models;
using SISAPI.Application.Repositories;
using SISAPI.Business;
using SISAPI.Domain.Entities;
using SISAPI.Domain.Entities.Common;
using SISAPI.Persistence;
using SISAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SISAPI.API.Controllers
{

    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(UserModel user)
        {

            if (ModelState.IsValid)
            {
                switch (user.UserType)
                {
                    case "student":

                        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

                        if (signInResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Students");
                        }
                        ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");

                        break;

                    case "academician":

                        break;

                    default:
                        break;

                }
            }
            return View(user);
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
