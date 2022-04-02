using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Domain.Entities.Common
{
    public class UserIdentityModel
    {
        public AppUser User { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
