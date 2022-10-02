using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository;
using CarRental.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using CarRental.API.interfaces;
using Autofac.Extras.Moq;
using CarRental.API.UtilitiesObjects;
using System.Net;
using CarRental.API.Controllers;

namespace BookingCarTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void BookingControllerCancelTrip()
        {
            using (var mock = AutoMock.GetLoose())
            {
                tripHeader tripHeader = new tripHeader(1, DateTime.Now, "test", DateTime.UtcNow, 23, 1, 2);
                Trip deletedTrip = new Trip();
                deletedTrip.TripId = 2;
                deletedTrip.TotalAmount = 200;
                deletedTrip.StartDate = DateTime.Today;
                deletedTrip.UserId = 1;
                deletedTrip.CarId = 1;
                deletedTrip.EndDate = DateTime.Now;
                DeletedTripObj deletedTripObj = new DeletedTripObj(200, deletedTrip);
                mock.Mock<IBookingRepository>().Setup(m => m.CancelTrip(It.IsAny<tripHeader>())).Returns(Task.FromResult(deletedTripObj));
                var cls = mock.Create<BookingController>();

                var expected = Task.FromResult(200);
                var actual = cls.CancelTrip(tripHeader);

                Assert.True(actual != null);

            }

        }



        [Fact]
        public void BookingControllerCarSelectedObject()
        {

            using (var mock = AutoMock.GetLoose())
            {

                CarSelectedObject carSelectedObject = new CarSelectedObject(12, 11, (CarModel)1, "1", 23, "test", "rtst", DateTime.Today, DateTime.Now);
                User user = new User();
                user.Email = "tset@gmail.com";
                user.FirstName = "test";
                user.LastName = "last";
                user.PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                user.PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                user.PhoneNumber = "1241252511";
                user.UserId = 12;

                User[] users = new User[1];
                users[0] = user;

                Trip trip = new Trip();

                mock.Mock<IBookingRepository>().Setup(m => m.ApplyBooking_and_confrimation(It.IsAny<CarSelectedObject>())).Returns(Task.FromResult(trip));
                mock.Mock<IBookingRepository>().Setup(m => m.CheckUserValid(It.IsAny<CarSelectedObject>())).Returns(user);
                mock.Mock<IBookingRepository>().Setup(m => m.CheckPostCarValid(It.IsAny<CarSelectedObject>())).Returns(users);
                var cls = mock.Create<BookingController>();

                var expected = Task.FromResult(trip);
                var actual = cls.ApplyBooking_and_Confirm(carSelectedObject);

                Assert.True(actual.Id >= 0);
            }
        }


    }
}

