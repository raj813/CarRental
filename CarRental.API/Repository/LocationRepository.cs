using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository
{
    public class LocationRepository: RepositoryAsync<Location>, ILocationRepository
    {
        private readonly CarRentalDbContext _db;
        public LocationRepository(CarRentalDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Location location)
        {
            var objFromDb = _db.Locations.FirstOrDefault(l => l.LocationId == location.LocationId);
            if (objFromDb != null)
            {
                objFromDb.Province = location.Province;
                objFromDb.City = location.City;

                _db.SaveChanges();
            }

        }
    }
}
