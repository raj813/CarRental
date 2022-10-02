using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UtilitiesObjects
{
    public class tripHeader
    {  /*
         Incoming Data Format:{
                "carId": 1,
                "endDate": "2021-08-14T00:00:00",
                "image": null,
                "numberPlate": "12AZX",
                "startDate": "2021-08-13T00:00:00",
                "totalAmount": 30,
                "tripId": 8002,
                "userId": 2
                }
        */
        public int carId { get; private set; }
        public DateTime endDate { get; private set; }
        public string numberPlate { get; private set; }
        public DateTime startDate { get; private set; }
        public int totalAmount { get; private set; }
        public int tripId { get; private set; }
        public int userId { get; private set; }

        public tripHeader(int carId, DateTime endDate, string numberPlate, DateTime startDate, int totalAmount, int tripId, int userId)
        {
            this.carId = carId;
            this.endDate = endDate;
            this.numberPlate = numberPlate;
            this.startDate = startDate;
            this.totalAmount = totalAmount;
            this.tripId = tripId;
            this.userId = userId;
    }
    }
}
