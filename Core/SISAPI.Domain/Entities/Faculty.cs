using SISAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SISAPI.Domain.Entities
{
    public class Faculty : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacultyId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; }
        public string DeanId { get; set; }
    }
}
