using SISAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities
{
    public class Absenteeism : BaseEntity
    {
        public string StudentNo { get; set; } = null!;
        public short LessonId { get; set; }
        public byte TheoreticalAbs { get; set; }
        public byte PracticalAbs { get; set; }
        public DateTime? Date { get; set; }
        public string Period { get; set; }
    }
}
