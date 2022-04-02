using SISAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities
{
    public class Note: BaseEntity
    {
        public string StudentNo { get; set; } = null!;
        public short LessonId { get; set; }
        public string? Period { get; set; }
        public double? MidtermExam { get; set; }
        public double? MakeUpExam1 { get; set; }
        public double? FinalExam { get; set; }
        public double? MakeUpExam2 { get; set; }
        public double? Average { get; set; }
        public double? LetterScore { get; set; }
        public bool? Status { get; set; }
    }
}
