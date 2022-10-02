using CarRental.API.Data;
using CarRental.API.Dtos;
using CarRental.API.Extensions;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// controller for fetching all the trips for user 
namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TripController : ControllerBase
    {
        
        private readonly ITripRepository tripRepository;
        public TripController( ITripRepository tripRepository) {
           
            this.tripRepository = tripRepository;
        }
        //fetch trip for users 
        [HttpGet]
        public async Task<ActionResult<List<TripDTO>>> getTrips(){
            try
            {
                var userid = User.GetUserId();
                var result = await tripRepository.GetUsersAsync(userid);




                return Ok(result);
            }
            catch (Exception e) {

                return BadRequest();
            }
        }
    }
}
