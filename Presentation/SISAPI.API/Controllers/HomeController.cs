using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using SISAPI.Domain.Entities;
using System.Threading.Tasks;
using SISAPI.API.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using SISAPI.Persistence.Contexts;

namespace SISAPI.API.Controllers
{

    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SISContext _context;

        public HomeController(ILogger<HomeController> logger, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel user)
        {

            if (ModelState.IsValid)
            {
                switch (user.UserType)
                {
                    case "student":

                        var signInResultSt = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

                        if (signInResultSt.Succeeded)
                        {
                            HttpContext.Session.SetString("student_no", user.UserName);
                            return RedirectToAction("Index", "Students");
                        }
                        ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");

                        break;

                    case "academician":

                        var signInResultAc = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

                        if (signInResultAc.Succeeded)
                        {
                            HttpContext.Session.SetString("academician_mail", user.UserName);
                            return RedirectToAction("Index", "Academics");
                        }
                        ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
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
