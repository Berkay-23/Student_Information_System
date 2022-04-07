using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SISAPI.API.Areas.Admin.Models;
using SISAPI.Domain.Entities;
using System.Threading.Tasks;

namespace SISAPI.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return View(new AdminModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminModel admin)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(admin.UserName, admin.Password, true, false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Main", new { area = "Admin" });
            }
            ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
            
            return View(admin);
        }
    }
}
