using CarRental.API.Dtos;
using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface IReviewRepository: IRepositoryAsync<Review>
    {
        void Update(Review review);
      
        void AddReview(Review review);
        
    }
}
