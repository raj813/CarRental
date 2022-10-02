using CarRental.API.Data;
using CarRental.API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CarRentalDbContext _db;
        public UnitOfWork(CarRentalDbContext db)
        {
            _db = db;
            Car = new CarRepository(_db);
            Location = new LocationRepository(_db);
            SP_Call = new SP_Call(_db);
            Review = new ReviewRepository(_db);
        }

        public ICarRepository Car { get; private set; }
        public ILocationRepository Location { get; private set; }
        public ISP_Call SP_Call { get; private set; }
        public IReviewRepository Review { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
