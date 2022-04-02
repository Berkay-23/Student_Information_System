using SISAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities
{
    public class LessonInformation: BaseEntity
    {
        public string StudentNo { get; set; } = null!;
        public string RegisteredCourses { get; set; }
        public string RetakeFailCourses { get; set; }
    }
}
