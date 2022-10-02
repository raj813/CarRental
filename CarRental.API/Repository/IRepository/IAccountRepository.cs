using CarRental.API.Dtos;
using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
   public  interface IAccountRepository
    {

       public  Task<User> GetUserAsync(LoginDTO loginDTO);
      public  Task<bool> AddUserAsync(User user);
        public Task<bool> GetUserexistAsync(string emailid);

    }
}
