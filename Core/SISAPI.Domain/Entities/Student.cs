using SISAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string StudentNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public byte GradeLevel { get; set; }
        public short? AdvisorId { get; set; }

    }
}
