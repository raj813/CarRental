using Amazon.Auth.AccessControlPolicy;
using Autofac.Extras.Moq;
using CarRental.API.Controllers;
using CarRental.API.Dtos;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingCarTestProject
{
   public  class Account_Unit_test
    {
        [Fact]
        public void getuser_test() {
            using (var mock = AutoMock.GetLoose()) {
                using var hmac = new HMACSHA512();
                
                LoginDTO loginDTO = new LoginDTO
                {
                    Email = "tset@gmail.com",
                    PasswordHash = "raj",
                    UserType = "User"
                };
                string emailid = "tset @gmail.com";
                User user = new User();
                user.Email = "tset@gmail.com";
                user.FirstName = "test";
                user.LastName = "last";
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("raj"));
                user.PasswordSalt = hmac.Key;
                user.PhoneNumber = "1241252511";
                user.UserType = "User";
                user.UserId = 12;
                User[] users = new User[1];
                users[0] = user;
                mock.Mock<IAccountRepository>().Setup(m => m.GetUserAsync(It.IsAny<LoginDTO>())).Returns(Task.FromResult(user));
                var cls = mock.Create<AccountController>();
                var expected = Task.FromResult(200);
                var actual = cls.login(loginDTO);
                Assert.True(actual != null);




            }
        }

        [Fact]
        public void UserExists_test() {
            using (var mock = AutoMock.GetLoose())
            {

                using var hmac = new HMACSHA512();
                string emailid = "tset @gmail.com";
                User user = new User();
                user.Email = "tset@gmail.com";
                user.FirstName = "test";
                user.LastName = "last";
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("raj"));
                user.PasswordSalt = hmac.Key;
                user.PhoneNumber = "1241252511";
                user.UserType = "User";
                user.UserId = 12;
                User[] users = new User[1];
                users[0] = user;
                mock.Mock<IAccountRepository>().Setup(m => m.GetUserexistAsync(It.IsAny<string>())).Returns(Task.FromResult(true));
                var cls = mock.Create<AccountController>();
                var expected = Task.FromResult(200);
                var actual = cls.UserExists(emailid);
                Assert.True(actual != null);


            }
        }
    }
}
