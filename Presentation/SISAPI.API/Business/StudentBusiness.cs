using SISAPI.API.Models.StudentModels;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System;

namespace SISAPI.API.Business
{
    public class StudentBusiness
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IGradeInformationRepository _gradeInformationRepository;
        private readonly ILessonInformationRepository _lessonInformationRepository;

        public StudentBusiness(IStudentRepository studentRepository, IAcademicRepository academicRepository, INoteRepository noteRepository, IGradeInformationRepository gradeInformationRepository, ILessonRepository lessonRepository, ILessonInformationRepository lessonInformationRepository)
        {
            _studentRepository = studentRepository;
            _academicRepository = academicRepository;
            _noteRepository = noteRepository;
            _lessonRepository = lessonRepository;
            _gradeInformationRepository = gradeInformationRepository;
            _lessonInformationRepository = lessonInformationRepository;
        }

        public async Task<StudentMainModel> GetStudentModelAsync(string student_no)
        {
            if (student_no != null)
            {
                Student student = await _studentRepository.GetByIdAsync(student_no);
                GradeInformation gradeInformation = await _gradeInformationRepository.GetByIdAsync(student_no);
                Academic academic = new Academic();

                if (student.AdvisorId != null)
                {
                    academic = await _academicRepository.GetByIdAsync((short)student.AdvisorId);
                }

                StudentMainModel model = new StudentMainModel()
                {
                    student = student,
                    advisor = academic,
                    gradeInformation = gradeInformation
                };
                return model;
            }
            return null;
        }

        public async Task<NoteModel> GetNoteModelAsync(int period_index, string student_no)
        {

            IEnumerable<Note> notes = _noteRepository.GetWhere(n => n.StudentNo == student_no);

            List<string> periods = new List<string>();

            foreach (Note note in notes)

                if (!periods.Contains(note.Period)) periods.Add(note.Period);

            if (periods.Count > period_index)
            {
                periods.Sort(); periods.Reverse();

                List<short> lesson_ids = new List<short>();
                List<Note> per_notes = new List<Note>();
                List<Lesson> lessons = new List<Lesson>();

                IEnumerable<Note> period_notes = _noteRepository.GetWhere(n => n.StudentNo == student_no && n.Period == periods[period_index]);

                foreach (Note note in period_notes)
                {
                    lesson_ids.Add(note.LessonId);
                    per_notes.Add(note);
                }

                foreach (short id in lesson_ids)
                {
                    Lesson l = await _lessonRepository.GetByIdAsync(id);

                    if (l != null) lessons.Add(l);
                }
                return new NoteModel() { Notes = per_notes, Lessons = lessons, Periods = periods, SelectedPeriod = period_index };
            }
            return null;
        }

        public async Task<ReceivedLessonsModel> GetLessonsAsync(int period_index, string student_no)
        {
            NoteModel noteModel = await GetNoteModelAsync(period_index, student_no);

            if (noteModel != null)
            {
                List<Lesson> lesson_list = new List<Lesson>();

                foreach (Lesson less in noteModel.Lessons)
                {
                    IEnumerable<Lesson> lessons = _lessonRepository.GetWhere(l => l.LessonCode == less.LessonCode);

                    foreach (Lesson lesson in lessons)
                    {
                        lesson_list.Add(lesson);
                    }
                }
                return new ReceivedLessonsModel() { Lessons = lesson_list, Periods = noteModel.Periods, SelectedPeriod = period_index };
            }
            return null;
        }

        public async Task<TranscriptInformationModel> GetTranscriptInformationModelAsync(string student_no)
        {
            TranscriptInformationModel model = new TranscriptInformationModel();

            List<List<Note>> notes = null;
            List<List<Lesson>> lessons = null;

            NoteModel note_model = await GetNoteModelAsync(0, student_no);

            if (note_model != null)
            {
                notes = new List<List<Note>>();
                lessons = new List<List<Lesson>>();

                for (int i = 0; i < note_model.Periods.Count; i++)
                {
                    NoteModel note = await GetNoteModelAsync(i, student_no);
                    notes.Add(note.Notes);
                    lessons.Add(note.Lessons);
                }
                notes.Reverse(); lessons.Reverse();
                model.Notes = notes;
                model.Lessons = lessons;
                model.StudentInfos = await GetStudentModelAsync(student_no);
            }
            return model;
        }

        public async Task<LessonEnrollmentModel> GetLessonEnrollmentModelAsync(string student_no)
        {
            LessonInformation lessonInformation = await _lessonInformationRepository.GetByIdAsync(student_no);
            LessonEnrollmentModel model = null;

            if (lessonInformation != null)
            {
                Student student = await _studentRepository.GetByIdAsync(student_no);
                List<Academic> academics = new List<Academic>();
                List<Lesson> lessons = new List<Lesson>();

                if (lessonInformation.RegisteredCourses == null)
                {
                    if (lessonInformation.RetakeFailCourses != null)
                    {
                        foreach (string lesson_id in lessonInformation.RetakeFailCourses.Trim().Split(" "))
                        {
                            Lesson lesson = await _lessonRepository.GetByIdAsync(short.Parse(lesson_id), false);

                            if (lesson.Semester == Program.NextSemester)
                                lessons.Add(lesson);
                        }
                    }
                    int counter = lessons.Count - 1;

                    if (student.GradeLevel != 4)
                    {
                        IEnumerable<Lesson> courses = _lessonRepository.GetWhere(l => l.Semester == Program.NextSemester && l.Department == student.Department && l.GradeLevel == student.GradeLevel + 1);

                        foreach (Lesson lesson in courses)
                            lessons.Add(lesson);
                    }

                    foreach (Lesson less in lessons)
                    {
                        if (less.LecturerId != null)
                        {
                            academics.Add(await _academicRepository.GetByIdAsync((short)less.LecturerId));
                        }
                        else
                        {
                            academics.Add(null);
                        }
                    }
                    model = new LessonEnrollmentModel() { Lessons = lessons, Student = student, Academics = academics, FailedCount = counter, Registration_Confirmation = false };
                }
                else
                {
                    foreach (string lesson_id in lessonInformation.RegisteredCourses.Trim().Split(" "))
                    {
                        Lesson lesson = await _lessonRepository.GetByIdAsync(short.Parse(lesson_id), false);
                        lessons.Add(lesson);
                    }

                    foreach (Lesson less in lessons)
                    {
                        if (less.LecturerId != null)
                        {
                            academics.Add(await _academicRepository.GetByIdAsync((short)less.LecturerId));
                        }
                        else
                        {
                            academics.Add(null);
                        }
                    }
                    model = new LessonEnrollmentModel() { Lessons = lessons, Student = student, Academics = academics, FailedCount = -99, Registration_Confirmation = lessonInformation.Registration_confirmation};
                }
            }
            return model;
        }

        public async Task<LessonEnrollmentModel> SetLessonEnrollmentAsync(IFormCollection collection, string student_no)
        {
            string registeredCourses = string.Empty;

            foreach (string key in collection.Keys)
            {
                if (key != "__RequestVerificationToken")
                {
                    registeredCourses = string.Concat(registeredCourses, $"{collection[key]} ");
                }
            }

            LessonInformation information = await _lessonInformationRepository.GetByIdAsync(student_no);
            information.RegisteredCourses = registeredCourses;
            await _lessonInformationRepository.SaveAsync();

            return await GetLessonEnrollmentModelAsync(student_no);
        }
    }
}
