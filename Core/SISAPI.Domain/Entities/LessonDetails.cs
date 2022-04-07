using SISAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace SISAPI.Domain.Entities
{
    public class LessonDetails: BaseEntity
    {
        [Key]
        public short LessonId { get; set; }
        public short MidtermPercentage { get; set; }
        public short FinalPercentage { get; set; }
        public bool Certainty_of_percentages { get; set; }
        public bool Certainty_of_midterm { get; set; }
        public bool Certainty_of_makeup1 { get; set; }
        public bool Certainty_of_final { get; set; }
        public bool Certainty_of_makeup2 { get; set; }
        public float AA_startingGrade { get; set; }
        public float BA_startingGrade { get; set; }
        public float BB_startingGrade { get; set; }
        public float CB_startingGrade { get; set; }
        public float CC_startingGrade { get; set; }

    }
}
