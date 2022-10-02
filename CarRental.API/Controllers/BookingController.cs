using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Repository;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using NSwag.Annotations;
using CarRental.API.UtilitiesObjects;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CarRental.API.Extensions;


namespace CarRental.API.Controllers
{
    [Route("api/[controller]")] // (/api/Booking)
    [ApiController]
   [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingRepository _bookingRepository;
       

        public BookingController(IBookingRepository bookingRepository , ILogger<BookingController> logger)
        {
            this._bookingRepository = bookingRepository;
            _logger = logger;
            _logger.LogInformation("C00N: C represents the Logger catgeory (e.g 200N is for BookingController catgeory)\n" + "\t N represents logger level:" +
                "\t 1 ==> success, 2 ==>debug \n" + "\t 3 ==> trace, 4 ==> warning\n" + "\t 5 ==> errors"); // to understand the logger id code
        }


        //Takes Form data to search the car list 
        [Route("BookingReservation")]
        [HttpGet]
        public ActionResult getFormData(string city, string startDate, string endDate, CarModel model)
        {
            FormData bookingFormData = new FormData(city, Convert.ToDateTime(startDate), Convert.ToDateTime( endDate), model);
            var _Action = CheckData(bookingFormData);

            if (_Action.GetType() == typeof(OkResult))
            {
                int userId = User.GetUserId();
                var carSelected = _bookingRepository.GetData(bookingFormData, userId);
                if (carSelected.GetType() == typeof(StatusCodeResult)) {
                    _logger.LogError(2005,"Can't connect to the db. Check the configuration default connection");
                    return carSelected;
                } // Can't connect to the db
                if (carSelected.GetType() == typeof(BadRequestObjectResult)) {

                    return carSelected;

                }// accepted but either the city is not found in db or the model for the choosen city is not found.
                _logger.LogInformation(2001, "successful request");
                return carSelected; // OKResult
            }
            return _Action;
        }


        //Takes the chosen car from the user tyo confirm trip reseravation  
        [Route("ConfirmTrip")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ApplyBooking_and_Confirm([FromBody] CarSelectedObject selected_car)
        {
            //check the Json data
            ActionResult statusCode = checkPostData(selected_car);
            if (statusCode.GetType() == typeof(NoContentResult))
            {
                var trip_result = await _bookingRepository.ApplyBooking_and_confrimation(selected_car);
                _logger.LogInformation(2001, $"Trip confirm ,for {selected_car.carId},from {selected_car.startDate} to {selected_car.endDate} for userID {selected_car.userId}");
                return Ok(trip_result);
            }
            
            return statusCode;
        }

        //Delete/ cancel a trip
        [Route("CancelTrip")]
        [HttpDelete]
        public async Task<ActionResult> CancelTrip([FromBody] tripHeader trip)
        {
            
            var deletedTrip = await _bookingRepository.CancelTrip(trip);
            if (deletedTrip.status == 500) {
                _logger.LogError(2005, "Can't connect to the db. Check the configuration default connection");
                return StatusCode(500);
             }
            if (deletedTrip.status == 400)
            {
                _logger.LogError(2005, "Trip Id is not correct...");
                return BadRequest("Trip Not Found. Check your trip ID");
            }
            _logger.LogInformation(2001, "successful request");
            return Ok(deletedTrip);
        }



        /*Behaviors for test and error cases handling, such as 
         * {
              "carId": 0,
              "model": 0,
              "modelValue": null,
              "pricePerDay": 0,
              "imge": null,
              "numberPlate": null,
              "startDate": "0001-01-01T00:00:00",
              "endDate": "0001-01-01T00:00:00"
            }
         */

        private ActionResult checkPostData(CarSelectedObject chosenObject)
        {
            // check UserID
            var userId_Check= _bookingRepository.CheckUserValid(chosenObject);

            //JSON data is not correct
            if (userId_Check.ToString().Length == 0)
            {
                _logger.LogError(2005, $"Invalid user Id or user not found .userID: {userId_Check}");
                return BadRequest("User id not found or not correct");
            }
            //can not connect to db 
            if (userId_Check == null)
            {
                _logger.LogError(2005, $"Not able to  conncet to database ,{StatusCode(500)}");
                return StatusCode(500);
            }

            // check startDate and endDate in case the client didn't send it
            var statusCode = CheckData(new FormData("None", chosenObject.startDate, chosenObject.endDate, chosenObject.model));
            if (statusCode.GetType() == typeof(BadRequestObjectResult))
            {
                _logger.LogError(2005, "invalid date entered");
                return BadRequest("Dates is requried and can't be in the past");
            }

            // check that the data send by client is valid by comparing them in db
            var queryCheck = _bookingRepository.CheckPostCarValid(chosenObject);
            //can not connect to db 
            if (queryCheck == null) 
            {
                _logger.LogError(2005, $"Not able to  conncet to database ,{StatusCode(500)}");
                return StatusCode(500);
            }
            //JSON data is not correct
            if (queryCheck.Length == 0)
            {
                _logger.LogError(2005, $"Incorrect json data found :{queryCheck}");
                return BadRequest("Car information is not correct, check your data values");
            }
            

            //var jsonStringnew = JsonConvert.SerializeObject(chosenObject).Length;
            return NoContent();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult CheckData(FormData form_data)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid modelstate");
                return BadRequest(ModelState);
            }
            else if (form_data._cityName == null || form_data._cityName.Trim().Length == 0)
            {
                _logger.LogError(2005, $"City is required");
                return BadRequest("City is required");
            }
            else if (form_data._startDate.ToString().Length == 0 || form_data._endDate.ToString().Length == 0)
            {
                _logger.LogError(2005, $"Date is required");
                return BadRequest("Date is required");
            }
            else if (form_data._startDate < DateTime.Today)
            {
                _logger.LogError(2005, $"Invalid start date found :{form_data._startDate}");
                return BadRequest("Start Date can't be in the past");
            }
            else if (form_data._endDate < DateTime.Today)
            {
                _logger.LogError(2005, $"Invalid end date found :{form_data._endDate}");
                return BadRequest("End Date can't be in the past");
            }
            else if (form_data._startDate > form_data._endDate)
            {
                _logger.LogError(2005, $"Start Date,{form_data._startDate} can't come after the End Date ,{form_data._endDate}");
                return BadRequest("Start Date can't come after the End Date");
            }
            return Ok();
        }
    }
}
