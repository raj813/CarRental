using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UtilitiesObjects
{
    public class FormData
    {
        public string _cityName { get; private set; }
        public DateTime _startDate { get; private set; }
        public DateTime _endDate { get; private set; }
        public CarModel _Model { get; private set; }


        public FormData(string cityName, DateTime startDate, DateTime endDate, CarModel Model)
        {
            this._cityName = cityName;
            this._startDate = startDate;
            this._endDate = endDate;
            this._Model = Model;
                
        }

        

    }
}
