using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Dtos
{
    public class ReviewDTO
    {
    
        public int FeedbackId { get; set; }

        public int StarValue { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

    }

    
}
