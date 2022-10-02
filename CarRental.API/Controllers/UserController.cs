using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using CarRental.API.Extensions;
using Microsoft.Extensions.Logging;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository,ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            var userId = User.GetUserId();

            User userRecord = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId == userId);

            if (userRecord == null)
            {
                _logger.LogWarning($"User profile does not exist for user id {userId}");
                return NotFound("User profile does not exist");
            }

            return userRecord;
        }

        //PUT: api/User
        [HttpPut]
        public async Task<IActionResult> PutUser(User user)
        {
            var userId = User.GetUserId();

           _userRepository.UpdateUser(user);

            try
            {
                await _userRepository.Save();
                _logger.LogInformation($"Updated user with user id {user.UserId}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _userRepository.DoesRecordExist(userId))
                {
                    _logger.LogWarning($"User profile does not exist for user id {userId}");
                    return NotFound("User profile does not exist");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
    }
}