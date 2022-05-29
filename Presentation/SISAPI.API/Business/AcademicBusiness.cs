using SISAPI.API.Models.StudentModels;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System;
using SISAPI.API.Models.AcademicModels;

namespace SISAPI.API.Business
{
    public class AcademicBusiness
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IGradeInformationRepository _gradeInformationRepository;
        private readonly ILessonInformationRepository _lessonInformationRepository;

        public AcademicBusiness(IStudentRepository studentRepository, IAcademicRepository academicRepository, INoteRepository noteRepository, IGradeInformationRepository gradeInformationRepository, ILessonRepository lessonRepository, ILessonInformationRepository lessonInformationRepository)
        {
            _studentRepository = studentRepository;
            _academicRepository = academicRepository;
            _noteRepository = noteRepository;
            _lessonRepository = lessonRepository;
            _gradeInformationRepository = gradeInformationRepository;
            _lessonInformationRepository = lessonInformationRepository;
        }

        public async Task<NotesModel> GetNotesModelAsync(short lesson_id)
        {
            List<Note> notes = _noteRepository.GetWhere(n => n.LessonId == lesson_id && n.Period == Program.Period).ToList();

            List<string> students = new List<string>();

            foreach (Note note in notes)
            {
                Student student = await _studentRepository.GetByIdAsync(note.StudentNo);
                students.Add($"{student.Name} {student.Surname}");
            }
           
            return new NotesModel() { Notes = notes, Student_names = students };
        }

        public async Task SetNotesAsync(IFormCollection collection)
        {
            short lesson_id = short.Parse(collection["lesson_id"]);

            foreach (string item in collection.Keys)
            {
                if(item != "__RequestVerificationToken" && item != "lesson_id")
                {
                    string[] splitted_key = item.Split("-");
                    string lesson_code = splitted_key[0];
                    string student_no = splitted_key[1];

                    double? point = null;

                    try
                    {
                        point = double.Parse(collection[item]);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }

                    Note note = await _noteRepository.GetSingleAsync(n => n.LessonId == lesson_id && n.StudentNo == student_no && n.Period == Program.Period);

                    if (note != null)
                    {
                        switch (lesson_code)
                        {
                            case "mid": // Vize
                                note.MidtermExam = point;
                                break;

                            case "mk1": // Mazeret
                                note.MakeUpExam1 = point;
                                break;

                            case "fin": // Final
                                note.FinalExam = point;
                                break;

                            case "mk2": // Bütünleme
                                note.MakeUpExam2 = point;
                                break;
                        }
                    }
                    await _noteRepository.SaveAsync();
                }
            }
        }
        public async Task UpdateRetakeFailCoursesAsync()
        {
            IEnumerable<LessonInformation> infos_base = _lessonInformationRepository.GetAll(false);
            List<LessonInformation> infos = new List<LessonInformation>();

            foreach (LessonInformation info in infos_base)
                infos.Add(info);

            List<string> lesson_ids = new List<string>();

            foreach (LessonInformation info in infos)
            {
                IEnumerable<Note> notes = _noteRepository.GetWhere(s => s.StudentNo == info.StudentNo && s.Status == false, false);

                string failed = string.Empty;
                foreach (Note note in notes)
                {
                    failed = string.Concat(failed, $"{note.LessonId} ");
                }
                lesson_ids.Add(failed);
            }

            for (int i = 0; i < infos.Count; i++)
                infos[i].RetakeFailCourses = lesson_ids[i];


            foreach (LessonInformation lesson in infos)
            {
                _lessonInformationRepository.Update(lesson);
                await _lessonInformationRepository.SaveAsync();
            }
        }

        public async Task UpdateECTSAsync(List<Student> students)
        {
            foreach (Student student in students)
            {
                IEnumerable<Note> query = _noteRepository.GetWhere(n => n.StudentNo == student.StudentNo);
                List<Note> notes = new List<Note>();

                foreach (Note note in query)
                    notes.Add(note);


                List<string> periods = new List<string>();

                foreach (Note note in notes)
                    if (!periods.Contains(note.Period))
                        periods.Add(note.Period);

                List<int> total_ects_list = new List<int>();

                periods.Sort();

                foreach (string period in periods)
                {
                    int total_ects = 0;

                    foreach (Note note in notes)
                    {
                        if (note.Period == period)
                        {
                            Lesson lesson = await _lessonRepository.GetByIdAsync(note.LessonId);
                            total_ects += lesson.Ects;
                        }
                    }
                    total_ects_list.Add(total_ects);
                }

                List<double> gpa_list = new List<double>();

                for (int i = 0; i < periods.Count; i++)
                {
                    double gpa = 0.0;

                    foreach (Note note in notes)
                    {
                        if (note.Period == periods[i])
                        {
                            Lesson lesson = await _lessonRepository.GetByIdAsync(note.LessonId);

                            if (note.LetterScore != 0 && note.LetterScore != null)
                                gpa += ((double)note.LetterScore * lesson.Ects) / total_ects_list[i];
                        }
                    }
                    gpa_list.Add(Math.Round(gpa, 2));
                }

                if (gpa_list.Count > 0)
                {
                    double transcript_note = Math.Round(gpa_list.Sum() / gpa_list.Count, 2);
                    string gpas = string.Join(" ", gpa_list);
                    int ects = total_ects_list.Sum();

                    GradeInformation information = await _gradeInformationRepository.GetByIdAsync(student.StudentNo);
                    information.TranscriptNote = transcript_note;
                    information.Gpas = gpas;
                    await _gradeInformationRepository.SaveAsync();
                }

            }
        }
    }
}
