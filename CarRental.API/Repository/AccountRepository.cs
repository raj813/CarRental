using CarRental.API.Data;
using CarRental.API.Dtos;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// repository pattern implementation for account controller
namespace CarRental.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CarRentalDbContext datacontext;
        public AccountRepository(CarRentalDbContext datacontext) {
            this.datacontext = datacontext;


        }
        // too add user
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                datacontext.Users.Add(user);
                return await datacontext.SaveChangesAsync() > 0;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
           
        }
        //too get user 
        public async Task<User> GetUserAsync(LoginDTO loginDTO)
        {
            try
            {
                var user = await datacontext.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);
                return user;
            }

            catch(Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> GetUserexistAsync(string emailid)
        {
            try
            {
                return await datacontext.Users.AnyAsync(x => x.Email == emailid);
            }
            catch(Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
