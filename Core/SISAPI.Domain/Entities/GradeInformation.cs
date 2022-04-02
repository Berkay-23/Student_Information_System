using SISAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities
{
    public class GradeInformation : BaseEntity
    {
        public string StudentNo { get; set; } = null!;
        public string Gpas { get; set; }
        public short TotalEcts { get; set; }
        public double TranscriptNote { get; set; }
    }
}
