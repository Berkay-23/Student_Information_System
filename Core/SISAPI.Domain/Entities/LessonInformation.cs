using SISAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISAPI.Domain.Entities
{
    public class LessonInformation: BaseEntity
    {
        [Key]
        [Column(Order = 0)]
        public string StudentNo { get; set; } = null!;
        public string RegisteredCourses { get; set; }
        public string RetakeFailCourses { get; set; }
        public bool Registration_confirmation { get; set; } = false;
    }
}
