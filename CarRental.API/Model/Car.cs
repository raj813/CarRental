using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Model
{
    public enum CarModel
    {
        Economy,
        Compact,
        Intermediate,
        Standard,
        Convertible,
        Sporty,
        Premium,
        Luxury
    }



    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required(ErrorMessage ="Please select car model")]
        public CarModel Model { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public Double PricePerDay { get; set; }

        public string Image { get; set; }   // store the image path 

        [Required(ErrorMessage ="Please enter valid numberplate")]
        public  string NumberPlate { get; set; }


        public bool IsRented { get; set; }

        [Display(Name ="Location")]
        public virtual int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

    }
}
