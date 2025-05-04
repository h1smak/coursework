using BlaBlaApi.Controllers;
using BlaBlaApi.DTOs;
using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaTest
{
    [TestClass]
    public class BookingControllerTests
    {
        private BookingController _controller;
        private Booking _testBooking;

        [TestInitialize]
        public void Setup()
        {
            _controller = new BookingController();

            var bookingsField = typeof(BookingController)
                .GetField("bookings", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var list = (List<Booking>)bookingsField.GetValue(null);
            list.Clear();

            _testBooking = new Booking
            {
                Id = Guid.NewGuid().ToString(),
                Status = BookingStatus.Pending,
            };
            list.Add(_testBooking);
        }

        [TestMethod]
        public void Confirm_ValidId_ReturnsOkWithUpdatedStatus()
        {
            var result = _controller.Confirm(_testBooking.Id) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var updatedBooking = result.Value as Booking;
            Assert.IsNotNull(updatedBooking);
            Assert.AreEqual(BookingStatus.Confirmed, updatedBooking.Status);
        }        
    }
}