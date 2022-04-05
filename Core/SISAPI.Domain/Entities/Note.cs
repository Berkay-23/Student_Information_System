using SISAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISAPI.Domain.Entities
{
    public class Note: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
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
