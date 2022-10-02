using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository
{
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        private readonly CarRentalDbContext _db;
        public UserRepository(CarRentalDbContext db) : base(db)
        {
            _db = db;
        }

        public void UpdateUser(User user)
        {
            var objFromDb = _db.Users.FirstOrDefault(u => u.UserId == user.UserId);

            if (objFromDb != null)
            {
                objFromDb.FirstName = user.FirstName;
                objFromDb.LastName = user.LastName;
                objFromDb.Email = user.Email;
                objFromDb.PhoneNumber = user.PhoneNumber;

                _db.SaveChanges();
            }
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
