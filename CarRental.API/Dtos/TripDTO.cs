using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Dtos
{
    public class TripDTO
    {
        public int UserId { get; set; }
        public int TripId { get; set; }

    
        public DateTime StartDate { get; set; }

     
        public DateTime EndDate { get; set; }


        public double TotalAmount { get; set; }

      
        public int CarId { get; set; }
        public string Image { get; set; }  

       
        public string NumberPlate { get; set; }

    }
}
