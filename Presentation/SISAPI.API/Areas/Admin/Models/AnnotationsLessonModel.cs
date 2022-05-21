using System.ComponentModel.DataAnnotations;

namespace SISAPI.API.Areas.Admin.Models
{
    public class AnnotationsLessonModel
    {
        public short LessonId { get; set; }

        [Required(ErrorMessage = "Lütfen Ders adını giriniz")]
        public string LessonName { get; set; }

        [Required(ErrorMessage = "Lütfen Ders kodunu giriniz")]
        public string LessonCode { get; set; }

        public string LessonType { get; set; }

        [Required]
        [Range(1, 40, ErrorMessage = "Bu alan {1} - {2} aralığında olmalıdır.")]
        public byte Ects { get; set; }

        [Required]
        [Range(1, 40, ErrorMessage = "Bu alan {1} - {2} aralığında olmalıdır.")]
        public byte Credit { get; set; }

        [Required]
        public string Semester { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "Bu alan {1} - {2} aralığında olmalıdır.")]
        public byte GradeLevel { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Bu alan {1} - {2} aralığında olmalıdır.")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Lütfen pozitif bir tam sayı giriniz")]
        public byte PraticalLimit { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Bu alan {1} - {2} aralığında olmalıdır.")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Lütfen pozitif bir tam sayı giriniz")]
        public byte TheoreticalLimit { get; set; }
        public short? LecturerId { get; set; }
    }
}
