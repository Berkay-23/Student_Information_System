using SISAPI.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SISAPI.API.Areas.Admin.Models
{
    public class AnnotationsStudentModel
    {
        [Required(ErrorMessage = "Öğrenci no boş bırakılamaz")]
        public string StudentNo { get; set; }

        [Required(ErrorMessage = "Öğrenci adı boş bırakılamaz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Öğrenci soyadı boş bırakılamaz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Fakülte alanı boş bırakılamaz")]
        public string Faculty { get; set; }

        [Required(ErrorMessage = "Bölüm alanı boş bırakılamaz")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Sınıf alanı boş bırakılamaz")]
        [Range(1, 6, ErrorMessage = "Sınıf alanı {1} ve {2} aralığında olmalı.")]
        public byte GradeLevel { get; set; }
        public short? AdvisorId { get; set; }
        public IEnumerable<Faculty> Faculties { get; set; }
    }
}
