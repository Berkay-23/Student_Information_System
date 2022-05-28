using SISAPI.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SISAPI.API.Areas.Admin.Models
{
    public class AnnotationsAcademicModel
    {
        public short AcademicianId { get; set; }
        
        [Required(ErrorMessage = "Mail alanı boş bırakılamaz")]
        public string Mail { get; set; } = null!;
        
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Soyisim alanı boş bırakılamaz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Fakülte alanı boş bırakılamaz")]
        public string Faculty { get; set; }

        [Required(ErrorMessage = "Bölüm alanı boş bırakılamaz")]
        public string Department { get; set; }
        public IEnumerable<Faculty> Faculties { get; set; }
    }
}
