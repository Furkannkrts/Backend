using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForRegisterDto:IDto
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
