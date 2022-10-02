using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using CarRental.API.UtilitiesObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;

namespace CarRental.API.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CarRentalDbContext carRentalContext;
        private readonly ILogger<BookingRepository> _logger;

        public BookingRepository(CarRentalDbContext carRentalContext, ILogger<BookingRepository> logger)
        {
            this.carRentalContext = carRentalContext;
            this._logger = logger;
        }

        public ActionResult GetData(FormData formdata, int userId )
        {
            var result = CarSet(formdata._cityName.ToLower(), formdata._startDate, formdata._endDate, formdata._Model, userId);
            return result;
        }

        public async Task<Trip> ApplyBooking_and_confrimation(CarSelectedObject chosenCar)
        {
            int TotalAmount = getTotalAmount(chosenCar);

            //Insert the confirmed trip in the trip entity
            var trip = await InsertIntoTripsDB(chosenCar, TotalAmount);

            //Get Single car which need to be updated and change the isRented field to true.
            await UpdateCarsDB(chosenCar.carId, true);

            return trip;
        }

        public async Task<DeletedTripObj> CancelTrip(tripHeader trip)
        {
            /*
                Incoming Data Format:{
                    "carId": 1,
                    "endDate": "2021-08-14T00:00:00",
                    "numberPlate": "qwer2345",
                    "startDate": "2021-08-12T00:00:00",
                    "totalAmount": 56,
                    "tripId": 4,
                    "userId": 2
                }​​​​​​​ 
            */

            //First that the trip Id if found delete it from db else throw error
            var deletedTrip = await DeleteFromTripsDB(trip.tripId);
            if (deletedTrip.status == 500) return deletedTrip;
            if (deletedTrip.status == 400) return deletedTrip;

            //secondly: change is rented of the canceled trip car to false
            await UpdateCarsDB(trip.carId, false);

            return deletedTrip;
        }



        /*start of Class's dependent behaviours*/

        // Here we are comparing the sent JSON object with the entity, in order to check if any data is changed
        public object[] CheckPostCarValid(CarSelectedObject chosenCar)
                {
                    try
                    {
                        var car = (from carT in carRentalContext.Cars
                                   where carT.CarId.Equals(chosenCar.carId) && carT.Model.Equals(chosenCar.model)
                                         && carT.PricePerDay.Equals(chosenCar.pricePerDay) && carT.Image.Equals(chosenCar.image)
                                         && carT.NumberPlate.Equals(chosenCar.numberPlate)
                                   select carT).ToArray();
                        return car;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

        // Here we are checking that the user is in our db
        public object CheckUserValid(CarSelectedObject userObject)
            {
                try
                {
                    var user = carRentalContext.Users.Where(u => u.UserId == userObject.userId).Select(u => u);
                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        //select list of cars from db
        private ActionResult CarSet(string city, DateTime startDate, DateTime endDate, CarModel model, int userId)
            {
                List<CarSelectedObject> _result = new List<CarSelectedObject>();
                try
                {
                _logger.LogInformation(3001, "Currently fetching the location collection data ...");
                    var location = from loc in carRentalContext.Locations
                                   where loc.City.Equals(city)
                                   select loc.LocationId;

                if (location.ToArray().Length == 0)
                {
                    _logger.LogInformation(3001, "Location data is successfully fetched, but no data found using the client filters.");
                    _logger.LogWarning(3004, "As a future work change Bad request to empty array...");
                    return new BadRequestObjectResult("No City Found!");
                }

                _logger.LogInformation(3001, "Location data is successfully fetched!");
                _logger.LogInformation(3001, "Currently fetching the Cars collection data ...");
                var cars = (from carT in carRentalContext.Cars
                                    join loc in (from loc in carRentalContext.Locations
                                                 where loc.City.Equals(city)
                                                 select loc.LocationId)
                                    on carT.LocationId equals loc
                                    where carT.Model.Equals(model) && carT.IsRented.Equals(false)
                                    select new { carId = carT.CarId, model = carT.Model, pricaPerDay = carT.PricePerDay, plate = carT.NumberPlate, image = carT.Image }).ToArray();
                if (cars.ToArray().Length == 0) {
                    _logger.LogInformation(3001, "Cars data is successfully fetched, but no data found using the client filters.");
                    _logger.LogWarning(3004, "As a future work change Bad request to empty array...");
                    return new BadRequestObjectResult("No Model in this city Found!"); 
                }
                foreach (var c in cars)
                    {
                        _result.Add(new CarSelectedObject(userId, c.carId, c.model, Enum.GetName(typeof(CarModel), c.model), c.pricaPerDay, c.image, c.plate, startDate, endDate));
                    }
                }
                catch (Exception error)
                {
                    return new StatusCodeResult(500);
                }
                return new OkObjectResult(_result);
            }

        //Update a car's isRented filed
        private async Task UpdateCarsDB(int chosenCarId, bool value) {
                var car = carRentalContext.Cars
                            .Where(c => c.CarId == chosenCarId)
                            .FirstOrDefault();
                car.IsRented = value;
                await carRentalContext.SaveChangesAsync();
               _logger.LogInformation(3001, "The car became available again to rent");
        }

         //Insert new trip in the trip entity
         private async Task<Trip> InsertIntoTripsDB(CarSelectedObject chosenCar, int total_amount)
            {
                _logger.LogInformation(3001, "Creating trip object to be inserted  ...");
                var trip = new Trip()
                {
                    StartDate = chosenCar.startDate,
                    EndDate = chosenCar.endDate,
                    TotalAmount = total_amount,
                    CarId = chosenCar.carId,
                    UserId = chosenCar.userId
                };

                await carRentalContext.Trips.AddAsync(trip);
                await carRentalContext.SaveChangesAsync();
                _logger.LogInformation(3001, "Trip object is successfully inserted");
                return trip;
            }
            
         //Delete from trip table
         private async Task<DeletedTripObj> DeleteFromTripsDB(int tripID)
         {
            Trip deletedTrip;
            try
            {
                deletedTrip = carRentalContext.Trips.Where(t => t.TripId == tripID).FirstOrDefault();
                if (deletedTrip != null)
                {
                    carRentalContext.Trips.Remove(deletedTrip);
                    await carRentalContext.SaveChangesAsync();
                    _logger.LogInformation(3001, "Trips Successfully Deleted!");
                    return new DeletedTripObj(200, deletedTrip);
                }
            } catch (Exception)
            {
                return new DeletedTripObj(500, null);
            }
                return new DeletedTripObj(400, deletedTrip); //if deletedTrip not found we will return empty object to differntiate between not found error and server error
         }

         //calculate TotalAmount = totalDays * priceperDay 
         private int getTotalAmount(CarSelectedObject chosenCar)
            {
                int TotalDays = (int)(chosenCar.endDate.Date - chosenCar.startDate.Date).TotalDays;
                return (TotalDays > 0)?(int)chosenCar.pricePerDay * TotalDays: (int)chosenCar.pricePerDay; //e.g 30 dollars/day * 3 days = 90 dollars.
            }

        

        /*end of Class's dependent behaviours*/
    }
}




