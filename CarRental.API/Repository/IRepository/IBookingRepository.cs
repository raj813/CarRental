using CarRental.API.Model;
using CarRental.API.UtilitiesObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface IBookingRepository
    {
        ActionResult GetData(FormData formData, int userID);
        Task<Trip> ApplyBooking_and_confrimation(CarSelectedObject chosenCar);
        Task<DeletedTripObj> CancelTrip(tripHeader trip);
        object[] CheckPostCarValid(CarSelectedObject chosenCar);
        object CheckUserValid(CarSelectedObject userObject);
    }
}

