using SISAPI.Domain.Entities;
using System.Collections.Generic;

namespace SISAPI.API.Areas.Admin.Models
{
    public class StudentDetailModel
    {
        //public Student student { get; set; }
        public AnnotationsStudentModel student { get; set; }
        public Academic academician { get; set; }
        public IEnumerable<Note> notes { get; set; }
        public IEnumerable<Academic> academics { get; set; }
    }
}
