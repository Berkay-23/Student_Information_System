using SISAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities
{
    public class UnsuccessfulStudent: BaseEntity
    {
        public string StudentNo { get; set; }
        public short LessonId { get; set; }
        public string Period { get; set; }
        public string TypeOfFailure { get; set; }
    }
}
