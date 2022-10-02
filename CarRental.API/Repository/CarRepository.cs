using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository
{
    public class CarRepository:RepositoryAsync<Car>,ICarRepository
    {
        private readonly CarRentalDbContext _db;
        public CarRepository(CarRentalDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Car car)
        {
            var objFromDb = _db.Cars.FirstOrDefault(c => c.CarId == car.CarId);

            if (objFromDb != null)
            {
                objFromDb.Model = car.Model;
                objFromDb.PricePerDay = car.PricePerDay;
                objFromDb.Image = car.Image;
                objFromDb.NumberPlate = car.NumberPlate;
                objFromDb.IsRented = car.IsRented;
                objFromDb.LocationId = car.LocationId;

                _db.SaveChanges();
            }
        }
    }
}
