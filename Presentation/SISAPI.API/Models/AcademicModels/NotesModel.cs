using SISAPI.Domain.Entities;
using System.Collections.Generic;

namespace SISAPI.API.Models.AcademicModels
{
    public class NotesModel
    {
        public List<Lesson> Lessons { get; set; }
        public List<Note> Notes { get; set; }
        public List<string> Student_names { get; set; }
        public short? Selected_lesson { get; set; }

    }
}
