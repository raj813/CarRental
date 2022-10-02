using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        [Required (ErrorMessage ="First name is required")]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
        public string UserType { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }


        public virtual ICollection<Trip> Trips { get; set; }
    }
}
