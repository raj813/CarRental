
using CarRental.API.Dtos;
using CarRental.API.Extensions;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;



        public ReviewController(IUnitOfWork unitOfWork, ILogger<ReviewController> logger, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userRepository = userRepository;
        }

        // GET: api/Review
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReview()
        {
            IEnumerable<Review> ReviewList = await _unitOfWork.Review.GetAllAsync(null, null, null);
            return new JsonResult(ReviewList);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"invalid ID  : {id} found . ");
                return BadRequest("Id cannot be less than 0");
            }

            Review review = await _unitOfWork.Review.GetFirstOrDefaultAsync(r => r.FeedbackId == id);


            if (review == null)
            {
                _logger.LogWarning($"Car recored not found for ID :{id}.");
                return NotFound("Car record does not exist");
            }

            return review;
        }

        // GET: api/Review/1
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(ReviewDTO reviewDTO)
        {
            reviewDTO.FeedbackId = 0;

            if (!ModelState.IsValid)
            {

                _logger.LogWarning($"Invalid modelstate");
                return BadRequest(ModelState);
            }
            
            if(reviewDTO.Title.Length <= 0)
            {
                _logger.LogWarning($"Title is required");
                return BadRequest(ModelState);

            }

            if (reviewDTO.FeedbackId < 0)
            {
                _logger.LogWarning($"Invalid Feedback Id received {reviewDTO.FeedbackId}");
                return BadRequest(ModelState);
            }

            if (reviewDTO.StarValue <= 0)
            {
                reviewDTO.StarValue = 4;
            }
            // add user access method from tocken
            // 

            var userId = 1;                
            try{
                userId = User.GetUserId();
            }
            catch
            {

            }

            Review reviewobj = new()
            {
                StarValue = reviewDTO.StarValue,
                Title = reviewDTO.Title,
                Description = reviewDTO.Description,
                PostTime = DateTime.Now,
                UserId = userId,
                User = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId == userId)

            };




            // _unitOfWork.Review.AddReview(reviewobj);
            await _unitOfWork.Review.AddAsync(reviewobj);
            await _unitOfWork.Review.Save();
            _logger.LogInformation($"new review added by :{reviewobj.User.FirstName} , user ID:{reviewobj.UserId}");
            return await GetReview(reviewobj.FeedbackId);
        }

        //PUT :api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id <= 0 || review.FeedbackId != id)
            {
                _logger.LogWarning($"Invlid id found :{id}");
                return BadRequest("Id is not valid or Feedback not found");
            }


            var userid = User.GetUserId();

            if (!(userid == review.UserId))
            {
                _logger.LogError($"wrong user try to  Edit review ");
                return StatusCode(401, "Access denied !");
            }


            _unitOfWork.Review.Update(review);


            try
            {
                _logger.LogInformation($"Review added for user :{review.UserId}");
                await _unitOfWork.Review.Save();    //Seve the review 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Review.DoesRecordExist(review.FeedbackId))
                {
                    _logger.LogWarning($"Review not found ,FeedBackId :{review.FeedbackId}");
                    return NotFound("review record does not exist or Not authorise to change");
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }


        // DELETE: api/Review/1
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteReview(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"Wrong Id received , Id :{id}");
                return BadRequest("Id cannot be less than 0");
            }


            Review review = await _unitOfWork.Review.GetAsync(id);

            var userid = User.GetUserId();

            if(!(userid == review.UserId))
            {
                _logger.LogError($"wrong user try to  delete review ");
                return StatusCode(401, "Access denied !"); 
            }

            if (review == null)
            {
                _logger.LogWarning($"empty object received ");
                return NotFound();
            }

            await _unitOfWork.Review.RemoveAsync(review);

            _logger.LogWarning($"REview removed ,Removed ID :{id}");

            await _unitOfWork.Review.Save();

            _logger.LogInformation($"Data updated");

            return NoContent();
        }





    }
}
