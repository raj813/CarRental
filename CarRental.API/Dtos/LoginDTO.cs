using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Dtos
{
    public class LoginDTO
    {

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserType { get; set; }
    }
}
