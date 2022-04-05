﻿using Microsoft.AspNetCore.Authorization;
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

            Note note = await _noteRepository.GetSingleAsync(n => n.StudentNo == id && n.LessonId == 42 && n.Period == "2021-Bahar", true);

            note.MakeUpExam2 = 61;
            int changed = await _noteRepository.SaveAsync();

            StudentDetailModel model = await GetStudentDetailModel(id);
            SetSession("student_no", id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StudentDetail(StudentDetailModel model, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                foreach (string key in collection.Keys)
                {
                    if (key!= "__RequestVerificationToken")
                    {
                        await UpdateNote(key, collection[key]);
                    }
                }
            }
            else
            {
                StudentDetailModel default_model = await GetStudentDetailModel(model.student.StudentNo);

                model.notes = default_model.notes;
                model.academics = default_model.academics;
                model.academician = default_model.academician;
            }

            return View(model);
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

        private async Task<StudentDetailModel> GetStudentDetailModel(string id)
        {
            Student student = await _studentRepository.GetByIdAsync(id, false);
            IEnumerable<Note> notes = _noteRepository.GetWhere(n => n.Period == "2021-Bahar" && n.StudentNo == student.StudentNo, false);
            Academic academician = await _academicRepository.GetByIdAsync((short)student.AdvisorId, false);
            IEnumerable<Academic> academics = _academicRepository.GetWhere(a => a.Department == student.Department, false);

            StudentDetailModel model = new StudentDetailModel()
            {
                student = new AnnotationsStudentModel()
                {
                    StudentNo = student.StudentNo,
                    Name = student.Name,
                    Surname = student.Surname,
                    AdvisorId = student.AdvisorId,
                    Department = student.Department,
                    Faculty = student.Faculty,
                    GradeLevel = student.GradeLevel
                },
                notes = notes,
                academician = academician,
                academics = academics
            };

            return model;
        }

        private void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
        private string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        private async Task<bool> UpdateNote(string key, string point)
        {
            string[] splitted_key = key.Split("-");
            string lesson_code = splitted_key[0];
            short lesson_id = short.Parse(splitted_key[1]);

            string student_no = GetSession("student_no");

            Note note = await _noteRepository.GetSingleAsync(n => n.StudentNo == student_no && n.LessonId == lesson_id && n.Period == "2021-Bahar", true);

            if(note != null)
            {
                switch (lesson_code)
                {
                    case "mid": // Vize
                        note.MidtermExam = double.Parse(point);
                        break;

                    case "mk1": // Mazeret
                        note.MakeUpExam1 = double.Parse(point);
                        break;

                    case "fin": // Final
                        note.FinalExam = double.Parse(point);
                        break;

                    case "mk2": // Bütünleme
                        note.MakeUpExam2 = double.Parse(point);
                        break;

                    default:
                        return false;
                }
            }
            await _noteRepository.SaveAsync();
            return true;
        }
    }
}
