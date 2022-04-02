using SISAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public short LessonId { get; set; }
        public string LessonName { get; set; }
        public string LessonCode { get; set; } = null!;
        public string LessonType { get; set; }
        public byte Ects { get; set; }
        public byte Credit { get; set; }
        public string Semester { get; set; }
        public byte GradeLevel { get; set; }
        public byte PraticalLimit { get; set; }
        public byte TheoreticalLimit { get; set; }
        public short? LecturerId { get; set; }

    }   
}
