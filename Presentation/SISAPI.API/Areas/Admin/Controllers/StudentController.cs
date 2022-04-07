using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SISAPI.API.Areas.Admin.Models;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SISAPI.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly INoteRepository _noteRepository;
        public StudentController(IStudentRepository studentRepository, IAcademicRepository academicRepository, INoteRepository noteRepository)
        {
            _studentRepository = studentRepository;
            _academicRepository = academicRepository;
            _noteRepository = noteRepository;
        }
        public IActionResult Index()
        {
            return View(_studentRepository.GetAll());
        }


        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            StudentDetailModel model = await GetStudentDetailModel(id);
            SetSession("student_no", id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(StudentDetailModel model, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                if (!collection.Keys.Contains("student.StudentNo")) // Öğrenci bilgilerini içermiyorsa (Not bilgilerini içerir)
                {
                    foreach (string key in collection.Keys)
                    {
                        if (key != "__RequestVerificationToken")
                        {
                            await UpdateNote(key, collection[key]);
                        }
                    }
                }
                else // Sadece Öğrenci bilgilerini içerir (Not bilgilerini içermez)
                {
                    Student student = await _studentRepository.GetByIdAsync(model.student.StudentNo);
                    student.Name = model.student.Name;
                    student.Surname = model.student.Surname;
                    student.Department = model.student.Department;
                    student.Faculty = model.student.Faculty;
                    student.GradeLevel = model.student.GradeLevel;
                    student.AdvisorId = model.student.AdvisorId;
                    await _studentRepository.SaveAsync();
                }

                return RedirectToAction("Details", new { id = GetSession("student_no") });
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


        private async Task<StudentDetailModel> GetStudentDetailModel(string id)
        {
            Student student = await _studentRepository.GetByIdAsync(id, false);
            IEnumerable<Note> notes = _noteRepository.GetWhere(n => n.Period == Program.Period && n.StudentNo == student.StudentNo, false);
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

            double? note_point = null;

            try
            {
                note_point = double.Parse(point);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            Note note = await _noteRepository.GetSingleAsync(n => n.StudentNo == student_no && n.LessonId == lesson_id && n.Period == Program.Period);

            if (note != null)
            {
                switch (lesson_code)
                {
                    case "mid": // Vize
                        note.MidtermExam = note_point;
                        break;

                    case "mk1": // Mazeret
                        note.MakeUpExam1 = note_point;
                        break;

                    case "fin": // Final
                        note.FinalExam = note_point;
                        break;

                    case "mk2": // Bütünleme
                        note.MakeUpExam2 = note_point;
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
