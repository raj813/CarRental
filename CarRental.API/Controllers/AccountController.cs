using CarRental.API.Data;
using CarRental.API.Dtos;
using CarRental.API.interfaces;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//this controller is responsible for creating and sigining in users it have two methods register for creating new user and login for authenticating user
namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        
        private readonly ItokenService ItokenService;
        private readonly IAccountRepository accountRepository;

        public AccountController( ItokenService ItokenService , ILogger<AccountController> logger, IAccountRepository accountRepository)
        {
            _logger = logger;
          
            this.ItokenService = ItokenService;
            this.accountRepository = accountRepository;
        }
        //for creating new user 
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> register(RegisterDTO registerDTO) {
            try
            {
                if (await UserExists(registerDTO.Email)) return BadRequest("Username is taken");
                using var hmac = new HMACSHA512();
                var Appuser = new User
                {
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.PasswordHash)),
                    PasswordSalt = hmac.Key,
                    FirstName = registerDTO.FirstName.ToLower(),
                    LastName = registerDTO.LastName.ToLower(),
                    Email = registerDTO.Email,
                    PhoneNumber = registerDTO.PhoneNumber,
                    UserType = "User"

                };
                //  datacontext.Users.Add(Appuser);
                //await datacontext.SaveChangesAsync();
                bool result = await accountRepository.AddUserAsync(Appuser);
          

            // _logger.LogInformation($"New user added ,Usre name : {Appuser.FirstName} ,User mail :{Appuser.Email} .");
            if (result == true) {


                return new UserDTO
                {
                    Email = Appuser.Email,
                    UserType = Appuser.UserType,
                    Token = ItokenService.CreateToken(Appuser)
                };
            }
            return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        // for authenticating user 
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> login(LoginDTO loginDTO) {
            try { 
            var user = await accountRepository.GetUserAsync(loginDTO);
            if (user == null) return Unauthorized("Invalid username or password");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.PasswordHash));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
            }

            return new UserDTO
            {
                Email = user.Email,
                UserType = user.UserType,
                Token = ItokenService.CreateToken(user)
            };
        }
          catch (Exception e)
            {
                Console.WriteLine(e);
               return Unauthorized("Invalid username");
    }

}
        [HttpGet]
    public async Task<bool> UserExists(string emailid)
        {
            return await accountRepository.GetUserexistAsync(emailid);
        }





    }
}
