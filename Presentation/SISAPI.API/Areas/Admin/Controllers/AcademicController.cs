using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SISAPI.API.Areas.Admin.Models;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SISAPI.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AcademicController : Controller
    {
        private readonly IAcademicRepository _academicRepository;
        private readonly IFacultyRepository _facultyRepository;
        private IEnumerable<Faculty> Faculties { get; set; }

        public AcademicController(IAcademicRepository academicRepository, IFacultyRepository facultyRepository)
        {
            _academicRepository = academicRepository;
            _facultyRepository = facultyRepository;
            Faculties = _facultyRepository.GetAll();
        }

        public IActionResult Index()
        {
            return View(_academicRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AnnotationsAcademicModel(){ Faculties = Faculties });
        }

        [HttpPost]
        public IActionResult Create(AnnotationsAcademicModel model)
        {
            if (ModelState.IsValid)
            {
                Academic academic = new Academic()
                {
                    Mail = model.Mail,
                    Name = model.Name,
                    Surname = model.Surname,
                    Faculty = model.Faculty,
                    Department = model.Department
                };

                _academicRepository.AddAsync(academic);
                _academicRepository.SaveAsync();

                return RedirectToAction(nameof(Create));
            }
            else
            {
                model.Faculties = Faculties;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(short id)
        {
            Academic academic = await _academicRepository.GetByIdAsync(id);

            AnnotationsAcademicModel model = new AnnotationsAcademicModel()
            {
                AcademicianId = academic.AcademicianId,
                Mail = academic.Mail,
                Name = academic.Name,
                Surname = academic.Surname,
                Faculty = academic.Faculty,
                Department = academic.Department,
                Faculties = Faculties
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(AnnotationsAcademicModel model)
        {
            if (ModelState.IsValid)
            {
                Academic academic = await _academicRepository.GetByIdAsync(model.AcademicianId);
                academic.Name = model.Name;
                academic.Surname = model.Surname;
                academic.Mail = model.Mail;
                academic.Faculty = model.Faculty;
                academic.Department = model.Department;
                await _academicRepository.SaveAsync();

            }
            model.Faculties = Faculties;
            return View(model);
        }

        public async Task<IActionResult> Delete(short academic_id)
        {
            await _academicRepository.RemoveAsync(academic_id);
            await _academicRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
