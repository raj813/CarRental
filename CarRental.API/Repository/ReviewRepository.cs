using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository
{
    public class ReviewRepository : RepositoryAsync<Review>, IReviewRepository
    {

        private readonly CarRentalDbContext _db;
        public ReviewRepository(CarRentalDbContext db) : base(db)
        {
            _db = db;
        }

        public void AddReview(Review review)
        {
           
            _db.Add(review);
            _db.SaveChanges();
        }

        

        public void Update(Review review)
        {
            var objFromDb = _db.Reviews.FirstOrDefault(r => r.FeedbackId == review.FeedbackId);

            if (objFromDb != null)
            {
                objFromDb.StarValue = review.StarValue;
                objFromDb.Title = review.Title;
                objFromDb.Description = review.Description;
                objFromDb.PostTime = DateTime.Now;
               
                _db.SaveChanges();
            }
        }

    }
}
