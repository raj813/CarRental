using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Model
{
    public enum ProvinceNames
    {
        Alberta,
        BritishColumbia,
        Manitoba,
        NewBrunswick,
        NewfoundlandAndLabrador,
        NorthwestTerritories,
        NovaScotia,
        Nunavut,
        Ontario,
        PrinceEdwardIsland,
        Quebec,
        Saskatchewan,
        Yukon
    }

    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public ProvinceNames Province { get; set; }

    }
}
