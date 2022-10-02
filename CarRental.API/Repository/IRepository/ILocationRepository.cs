using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface ILocationRepository : IRepositoryAsync<Location>
    {
        void Update(Location location);
    }
}
