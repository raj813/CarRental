using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UtilitiesObjects
{
    public class DeletedTripObj
    {
        public int status { get; private set; }
        public Trip deletedTrip { get; private set; }

        public DeletedTripObj(int status, Trip deletedTrip)
        {
            this.status = status;
            this.deletedTrip = deletedTrip;
        }
    }
}
