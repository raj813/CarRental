using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UtilitiesObjects
{
    public class CarSelectedObject
    {
        public int userId { get; private set;} // when we send the car list will be -1 for booking, but will have a value when the user choose a car in confromTrip post 
        public int carId { get; private set; }
        public CarModel model { get; private set; }
        public string modelValue { get; private set; }
        public Double pricePerDay { get; private set; }

        public string image { get; private set; }
        public string numberPlate { get; private set; }
        public DateTime startDate { get; private set; }
        public DateTime endDate { get; private set; }


        public CarSelectedObject(int userId, int carId, CarModel model, string modelValue, Double pricePerDay, string image, string numberPlate, DateTime startDate, DateTime endDate)
        {
            this.userId = userId;
            this.carId = carId;
            this.model = model;
            this.modelValue = modelValue;
            this.pricePerDay = pricePerDay;
            this.image = image;
            this.numberPlate = numberPlate;
            this.startDate = startDate;
            this.endDate = endDate;

        }
    }
}
