using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SISAPI.API.Areas.Admin.Models;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SISAPI.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class SisSettingsController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly INoteRepository _noteRepository;

        public SisSettingsController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IStudentRepository studentRepository,
            IAcademicRepository academicRepository,
            INoteRepository noteRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _studentRepository = studentRepository;
            _academicRepository = academicRepository;
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Students()
        {
            return View(_studentRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> StudentDetail(string id)
        {
            Student student = await _studentRepository.GetByIdAsync(id, false);
            IEnumerable<Note> notes = _noteRepository.GetWhere(n => n.Period == "2021-Bahar" && n.StudentNo == student.StudentNo, false);
            Academic academician = await _academicRepository.GetByIdAsync((short)student.AdvisorId, false);
            IEnumerable<Academic> academics = _academicRepository.GetWhere(a => a.Department == student.Department, false);


            StudentDetailModel model = new StudentDetailModel() { 
                student = student,
                notes = notes,
                academician = academician,
                academics = academics
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult StudentDetail(StudentDetailModel model, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in collection)
                {
                    Debug.WriteLine(item);
                }
            }
            
            return View(new StudentDetailModel());
        }

        public IActionResult Academics()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }


    }
}
