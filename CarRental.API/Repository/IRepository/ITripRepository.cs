using CarRental.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface ITripRepository
    {
       public Task<IEnumerable<TripDTO>> GetUsersAsync(int userid);
    }
   
}
