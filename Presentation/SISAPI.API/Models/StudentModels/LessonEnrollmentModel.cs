using SISAPI.Domain.Entities;
using System.Collections.Generic;

namespace SISAPI.API.Models.StudentModels
{
    public class LessonEnrollmentModel
    {
        public Student Student { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Academic> Academics { get; set; }
        public int FailedCount { get; set; }
        public bool Registration_Confirmation { get; set; }
    }
}
