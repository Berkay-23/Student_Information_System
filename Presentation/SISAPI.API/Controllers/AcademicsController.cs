using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SISAPI.API.Business;
using SISAPI.API.Models.AcademicModels;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SISAPI.API.Controllers
{
    [Authorize(Roles = "Academician")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AcademicsController : Controller
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IGradeInformationRepository _gradeInformationRepository;
        private readonly ILessonInformationRepository _lessonInformationRepository;
        private AcademicBusiness _business;

        private static bool isLoggedIn { get; set; } = false;
        private static short _academician_id { get; set; }

        public AcademicsController(IStudentRepository studentRepository, IAcademicRepository academicRepository, INoteRepository noteRepository, ILessonRepository lessonRepository, IGradeInformationRepository gradeInformationRepository, ILessonInformationRepository lessonInformationRepository)
        {
            _studentRepository = studentRepository;
            _academicRepository = academicRepository;
            _noteRepository = noteRepository;
            _lessonRepository = lessonRepository;
            _gradeInformationRepository = gradeInformationRepository;
            _lessonInformationRepository = lessonInformationRepository;

            _business = new AcademicBusiness(_studentRepository, _academicRepository, _noteRepository, _gradeInformationRepository, _lessonRepository, _lessonInformationRepository);
        }

        public async Task<IActionResult> Index()
        {
            Academic model = await _academicRepository.GetSingleAsync(a => a.Mail == GetSession("academician_mail"));

            if (model != null)
            {
                _academician_id = model.AcademicianId;
                isLoggedIn = true;

                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Notes()
        {  
            if (isLoggedIn)
            {
                 List<Lesson> lessons = _lessonRepository.GetWhere(l => l.LecturerId == _academician_id).ToList();
                
                return View(new NotesModel() { Lessons = lessons });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Notes(short lesson_id, IFormCollection collection)
        {  
            if (isLoggedIn)
            {
                NotesModel model = await _business.GetNotesModelAsync(lesson_id);
                List<Lesson> lessons = _lessonRepository.GetWhere(l => l.LecturerId == _academician_id).ToList();
                model.Lessons = lessons;

                foreach (Lesson lesson in lessons)
                    if (lesson_id == lesson.LessonId)
                        model.Selected_lesson = lesson.LessonId;

                await _business.SetNotesAsync(collection);

                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Absences()
        {
            if (isLoggedIn)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LessonEnrollment()
        {
            if (isLoggedIn)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        private string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }
    }
}
