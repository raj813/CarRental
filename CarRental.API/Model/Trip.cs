using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Model
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        [Required(ErrorMessage ="Enter valid start start")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date(MM/dd/yyyy)")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage ="Enter valid End start")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date(MM/dd/yyyy)")]
        public DateTime EndDate { get; set; }

      
        public double TotalAmount { get; set; }

        [Display(Name = "Car")]
        public virtual int CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
        
        [Display(Name = "User")]
        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }


    }
}
