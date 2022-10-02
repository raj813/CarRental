using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CarRental.API.Extensions;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CarRentalDbContext _dbContext;
        private readonly IUserRepository _userRepository;



        public CarsController(IUnitOfWork unitOfWork, CarRentalDbContext dbContext , ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
            _userRepository = userRepository;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<IEnumerable<Car>> GetCars()
        {
            try
            {
                var userId = User.GetUserId();
                User userRecord = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId == userId);
                if (userRecord.UserType == "Admin")
                {
                    IEnumerable<Car> carsList = await _unitOfWork.Car.GetAllAsync(null, null, nameof(Location));
                    return carsList;
                }
                else
                {
                    IEnumerable<Car> emptyCar = new List<Car>();
                    _logger.LogWarning("User without Admin privileges tried to access");
                    return emptyCar;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error from getCars in car controller");
                _logger.LogError(ex.Message);
                IEnumerable<Car> emptyCar = new List<Car>();
                return emptyCar;
            }

 
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"invalid ID  : {id} found . ");
                return BadRequest("Id cannot be less than 0");
            }

            try
            {
                Car carRecord = await _unitOfWork.Car.GetFirstOrDefaultAsync(m => m.CarId == id, nameof(Location));
                //var car = await _context.Cars.FindAsync(id);

                if (carRecord == null)
                {
                    _logger.LogWarning($"Car recored not found for ID :{id}.");
                    return NotFound("Car record does not exist");
                }

                return carRecord;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error from getCar in car controller");
                _logger.LogError(ex.Message);
                return BadRequest("Exception occurred");
            }

        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId || id <= 0)
            {
                _logger.LogWarning($"invalid ID  : {id} found . ");
                return BadRequest("Id is not valid");
            }
            try
            {

                _dbContext.Update(car);

                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"car Updated to database ,for carID :{car.CarId}");
            }
            catch (Exception e)
            {

                if (await _unitOfWork.Car.DoesRecordExist(id))
                {
                    _logger.LogWarning($"Car recored not found for the ID : {car.CarId} and car : {car} .Error message :{e.Message}");
                    return NotFound("Car record does not exist");
                }
                else
                {
                    throw;
                }

                return BadRequest();

            }

            //_unitOfWork.Car.Update(car);

            //try
            //{
            //    await _unitOfWork.Car.Save();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (await _unitOfWork.Car.DoesRecordExist(id))
            //    {
            //        return NotFound("Car record does not exist");
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            //var test = await GetCar(id);
            return NoContent();
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            car.CarId = 0;
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid state Model");
                return BadRequest("Invalid data");
            }
            if (car.CarId < 0 || car.LocationId < 0)
            {
                _logger.LogWarning($"invalid Car ID  : {car.CarId} found . ");
                return BadRequest("CarId should not be less than 0 and location Id Should not be less than 0");
            }

            Location location = await _unitOfWork.Location.GetFirstOrDefaultAsync(l => l.City == car.Location.City && l.Province == car.Location.Province);
            //Location location = await _unitOfWork.Location.GetAsync(car.LocationId);

            if (location != null)
            {

                //await _unitOfWork.Location.AddAsync(car.Location);
                //await _unitOfWork.Location.Save();
                //Location locationInserted = await _unitOfWork.Location.GetFirstOrDefaultAsync(l => l.City == car.Location.City && l.Province == car.Location.Province);
                //car.LocationId = locationInserted.LocationId;
                //car.Location.LocationId = locationInserted.LocationId;
                //car.Location.Province = locationInserted.Province;
                //car.Location.City = locationInserted.City;

                car.LocationId = location.LocationId;
                car.Location = null;
            }
            try
            {
                await _unitOfWork.Car.AddAsync(car);
                await _unitOfWork.Car.Save();
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception from PostCar in car controller");
                _logger.LogError(ex.Message);
            }

            _logger.LogInformation($"Car save to databse ,for CarId:{car.CarId} .");
            return await GetCar(car.CarId);

            //return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"invalid Car ID  : {id} found . ");
                return BadRequest("Id cannot be less than 0");
            }

            Car car = await _unitOfWork.Car.GetAsync(id);

            if (car == null)
            {
                _logger.LogWarning($"Car not found for ID :{id}");
                return NotFound();
            }

            try
            {
                await _unitOfWork.Car.RemoveAsync(car);
                await _unitOfWork.Car.Save();
                _logger.LogInformation($"Car saved to database for CarId : {car.CarId}");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exceptions in Delete car when tried to delete id {id}");
                _logger.LogError(ex.Message);
            }

            return NoContent();
        }
    }
}
