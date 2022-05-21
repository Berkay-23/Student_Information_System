using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SISAPI.API.Areas.Admin.Models;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System.Threading.Tasks;

namespace SISAPI.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]

    public class LessonController : Controller
    {
        private readonly ILessonRepository _lessonRepository;
        public LessonController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public IActionResult Index()
        {
            return View(_lessonRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Lesson lesson = await _lessonRepository.GetByIdAsync(short.Parse(id));

            if (lesson != null)
            {
                return View(new AnnotationsLessonModel()
                {
                    LessonId = lesson.LessonId,
                    LessonCode = lesson.LessonCode,
                    LessonName = lesson.LessonName,
                    LessonType = lesson.LessonType,
                    Ects = lesson.Ects,
                    Credit = lesson.Credit,
                    Semester = lesson.Semester,
                    GradeLevel = lesson.GradeLevel,
                    TheoreticalLimit = lesson.TheoreticalLimit,
                    PraticalLimit = lesson.PraticalLimit,
                    LecturerId = lesson.LecturerId,
                });
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AnnotationsLessonModel model)
        {
            if (ModelState.IsValid)
            {
                Lesson lesson = await _lessonRepository.GetByIdAsync(model.LessonId);
                lesson.LessonCode = model.LessonCode;
                lesson.LessonName = model.LessonName;
                lesson.LessonType = model.LessonType;
                lesson.Ects = model.Ects;
                lesson.Credit = model.Credit;
                lesson.Semester = model.Semester;
                lesson.GradeLevel = model.GradeLevel;
                lesson.TheoreticalLimit = model.TheoreticalLimit;
                lesson.PraticalLimit = model.PraticalLimit;
                lesson.LecturerId = model.LecturerId;
                await _lessonRepository.SaveAsync();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AnnotationsLessonModel());
        }

        [HttpPost]
        public IActionResult Create(AnnotationsLessonModel model)
        {
            if (ModelState.IsValid)
            {
                Lesson lesson = new Lesson() {
                    LessonName = model.LessonName,
                    LessonCode = model.LessonCode,
                    LessonType = model.LessonType,
                    Credit = model.Credit,
                    Semester = model.Semester,
                    GradeLevel = model.GradeLevel,
                    TheoreticalLimit = model.TheoreticalLimit,
                    PraticalLimit = model.PraticalLimit,
                    Ects = model.Ects,
                    LecturerId = model.LecturerId
                };

                _lessonRepository.AddAsync(lesson);
                _lessonRepository.SaveAsync();

                return RedirectToAction(nameof(Create));
            }

            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int lesson_id)
        {
            await _lessonRepository.RemoveAsync(lesson_id);
            await _lessonRepository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
