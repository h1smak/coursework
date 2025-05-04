using BlaBlaApi.Controllers;
using BlaBlaApi.DTOs;
using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaTest
{
    [TestClass]
    public class DriverControllerTests
    {
        private DriverController _controller;
        private Trip _testTrip;
        private Booking _testBooking;

        [TestInitialize]
        public void Setup()
        {
            _controller = new DriverController();

            var bookingsField = typeof(DriverController)
                .GetField("bookings", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var tripsField = typeof(DriverController)
                .GetField("trips", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            var bookingsList = (List<Booking>)bookingsField.GetValue(null);
            var tripsList = (List<Trip>)tripsField.GetValue(null);

            bookingsList.Clear();
            tripsList.Clear();

            _testTrip = new Trip
            {
                Id = Guid.NewGuid().ToString(),
                AvailableSeats = 3,
                Passengers = new List<Passenger>()
            };
            _testBooking = new Booking
            {
                Id = Guid.NewGuid().ToString(),
                Status = BookingStatus.Pending,
                Trip = _testTrip
            };

            tripsList.Add(_testTrip);
            bookingsList.Add(_testBooking);
        }

        [TestMethod]
        public void ConfirmRequest_ValidBookingAndSeats_ReturnsOkAndDecrementsSeats()
        {
            var result = _controller.ConfirmRequest(_testBooking.Id) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var booking = result.Value as Booking;
            Assert.AreEqual(BookingStatus.Confirmed, booking.Status);
            Assert.AreEqual(2, _testTrip.AvailableSeats);
        }

        [TestMethod]
        public void CancelTrip_TripWithoutPassengers_RemovesTrip()
        {
            var result = _controller.CancelTrip(_testTrip.Id) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var tripsField = typeof(DriverController)
                .GetField("trips", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var tripsList = (List<Trip>)tripsField.GetValue(null);

            Assert.IsFalse(tripsList.Any(t => t.Id == _testTrip.Id));
        }
    }
}