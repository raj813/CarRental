using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Dtos
{
    public class RegisterDTO
    {
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string   LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
    }
}
