using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNDSystem.Core.Dto.Auth
{
    public class CreateUserDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
