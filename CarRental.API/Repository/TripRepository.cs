using CarRental.API.Data;
using CarRental.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public class TripRepository : ITripRepository
    {
        private readonly CarRentalDbContext datacontext;
        public TripRepository(CarRentalDbContext datacontext) {
            this.datacontext = datacontext;
        }
        public async Task<IEnumerable<TripDTO>> GetUsersAsync(int userid)
        {
            try
            {
                var result = (
                   from d in datacontext.Cars

                   join e in (from e in datacontext.Trips
                              where e.UserId.Equals(userid)
                              select new
                              {
                                  e.UserId,
                                  e.TripId,
                                  e.StartDate,
                                  e.EndDate,
                                  e.TotalAmount,

                                  e.CarId
                              })
                           on d.CarId equals e.CarId
                   select new TripDTO
                   {
                       UserId = e.UserId,
                       TripId = e.TripId,
                       StartDate = e.StartDate,
                       EndDate = e.EndDate,
                       TotalAmount = e.TotalAmount,

                       CarId = e.CarId,
                       Image = d.Image,


                       NumberPlate = d.NumberPlate




                   }).ToList();
                return result;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
