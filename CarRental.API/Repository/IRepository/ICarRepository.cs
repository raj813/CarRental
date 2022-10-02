using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface ICarRepository:IRepositoryAsync<Car>
    {
        void Update(Car car);
    }
}
