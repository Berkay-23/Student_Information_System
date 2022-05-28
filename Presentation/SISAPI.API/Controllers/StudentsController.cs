using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SISAPI.API.Business;
using SISAPI.API.Models.StudentModels;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SISAPI.API.Controllers
{
    [Authorize(Roles = "Student")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IGradeInformationRepository _gradeInformationRepository;
        private readonly ILessonInformationRepository _lessonInformationRepository;
        private StudentBusiness _business;

        private static bool isLoggedIn { get; set; } = false;
        private static string _student_no { get; set; }

        public StudentsController(IStudentRepository studentRepository, IAcademicRepository academicRepository, INoteRepository noteRepository, IGradeInformationRepository gradeInformationRepository, ILessonRepository lessonRepository, ILessonInformationRepository lessonInformationRepository)
        {
            _studentRepository = studentRepository;
            _academicRepository = academicRepository;
            _noteRepository = noteRepository;
            _lessonRepository = lessonRepository;
            _gradeInformationRepository = gradeInformationRepository;
            _lessonInformationRepository = lessonInformationRepository;

            _business = new StudentBusiness(_studentRepository, _academicRepository, _noteRepository, _gradeInformationRepository, _lessonRepository, _lessonInformationRepository);
        }

        public async Task<IActionResult> Index()
        {
            _student_no = GetSession("student_no");
            StudentMainModel model = await _business.GetStudentModelAsync(_student_no);

            if (model != null)
            {
                isLoggedIn = true;
                
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcademicCalendar() // Akademik Takvim
        {
            if (isLoggedIn)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConsultantInformation() // Danışman Bilgileri
        {
            if (isLoggedIn)
            {
                return View(await _business.GetStudentModelAsync(_student_no));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ReceivedLessons() // Alınan  Dersler
        {
            if (isLoggedIn)
            {
                ReceivedLessonsModel model = await _business.GetLessonsAsync(0, _student_no);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ReceivedLessons(int selected) // Alınan  Dersler
        {
            ReceivedLessonsModel model = await _business.GetLessonsAsync(selected, _student_no);
            return View(model);
        }

        public IActionResult ExamSchedule() // Sınav Takvimi
        {
            if (isLoggedIn)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult InternshipInformation() // Staj Bilgileri
        {
            if (isLoggedIn)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LessonEnrollment() // Ders Kaydı
        {
            if (isLoggedIn)
            {
                LessonEnrollmentModel model = await _business.GetLessonEnrollmentModelAsync(_student_no);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LessonEnrollment(IFormCollection collection) // Ders Kaydı
        {
            if (isLoggedIn)
            {
                return View(await _business.SetLessonEnrollmentAsync(collection, _student_no));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> NoteList() // Not Listesi
        {
            if (isLoggedIn)
            {
                NoteModel model = await _business.GetNoteModelAsync(0, _student_no);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> NoteList(int selected) // Not Listesi
        {
            if (isLoggedIn)
            {
                NoteModel model = await _business.GetNoteModelAsync(selected, _student_no);
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> TranscriptInformation() // Transkript Bilgisi
        {
            if (isLoggedIn)
            {
                return View(await _business.GetTranscriptInformationModelAsync(_student_no));
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AbsenceStatus() // Devamsızlık Durumu
        {
            if (isLoggedIn)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        private string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }
    }
}
