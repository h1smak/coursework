using BlaBlaApi.Controllers;
using BlaBlaApi.DTOs;
using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaTest
{
    [TestClass]
    public class PassengerControllerTests
    {
        private PassengerController _controller;
        private Trip _trip;
        private Booking _booking;

        [TestInitialize]
        public void Setup()
        {
            _controller = new PassengerController();

            var bookingsField = typeof(PassengerController)
                .GetField("bookings", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var bookingsList = (List<Booking>)bookingsField.GetValue(null);
            bookingsList.Clear();

            _trip = new Trip
            {
                Id = Guid.NewGuid().ToString(),
                AvailableSeats = 2,
                Passengers = new List<Passenger>()
            };

            _booking = new Booking
            {
                Id = Guid.NewGuid().ToString(),
                Trip = _trip,
            };
        }

        [TestMethod]
        public void BookPlace_WithAvailableSeats_ReturnsOkAndDecreasesSeats()
        {
            var result = _controller.BookPlace(_booking) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var booked = result.Value as Booking;
            Assert.AreEqual(BookingStatus.Pending, booked.Status);
            Assert.AreEqual(1, _trip.AvailableSeats);
        }

        [TestMethod]
        public void LeaveReview_ValidRating_ReturnsOk()
        {
            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                Rating = 5,
                Text = "Все супер!"
            };

            var result = _controller.LeaveReview(review) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(review, result.Value);
        }
    }
}