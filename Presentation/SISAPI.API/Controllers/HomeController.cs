using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using SISAPI.Domain.Entities;
using System.Threading.Tasks;
using SISAPI.API.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Index(UserModel user)
        {

            if (ModelState.IsValid)
            {
                switch (user.UserType)
                {
                    case "student":

                        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

                        if (signInResult.Succeeded)
                        {
                            HttpContext.Session.SetString("student_no", user.UserName);
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
