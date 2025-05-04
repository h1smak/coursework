using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private static List<Trip> trips = new();
        private static List<Booking> bookings = new();

        [HttpPost("confirm-request/{bookingId}")]
        public IActionResult ConfirmRequest(string bookingId)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null || booking.Trip.AvailableSeats <= 0)
                return BadRequest("Немає вільних місць або бронювання не знайдено");

            booking.Status = BookingStatus.Confirmed;
            booking.Trip.AvailableSeats--;
            return Ok(booking);
        }

        [HttpPost("reject-request/{bookingId}")]
        public IActionResult RejectRequest(string bookingId)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return NotFound();

            booking.Status = BookingStatus.Rejected;
            return Ok();
        }

        [HttpDelete("cancel-trip/{tripId}")]
        public IActionResult CancelTrip(string tripId)
        {
            var trip = trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null || trip.Passengers.Count > 0)
                return BadRequest("Неможливо скасувати поїздку");

            trips.Remove(trip);
            return Ok();
        }
    }
}
