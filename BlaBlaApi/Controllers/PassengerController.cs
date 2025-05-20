using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengerController : ControllerBase
    {
        [HttpPost("book")]
        public IActionResult BookPlace([FromBody] Booking booking)
        {
            var trip = DataStore.Trips.FirstOrDefault(t => t.Id == booking.Trip.Id);
            if (trip == null)
                return NotFound("Поїздка не знайдена");

            var existingBooking = DataStore.Bookings.FirstOrDefault(b =>
                b.Trip.Id == booking.Trip.Id && b.Passenger.Id == booking.Passenger.Id);
            if (existingBooking != null)
                return BadRequest("Пасажир вже забронював цю поїздку");

            if (trip.AvailableSeats <= 0)
                return BadRequest("Місць немає");

            booking.Status = BookingStatus.Pending;
            DataStore.Bookings.Add(booking);
            trip.AvailableSeats--;
            trip.Passengers.Add(booking.Passenger);

            return Ok(booking);
        }

        [HttpPost("cancel/{id}")]
        public IActionResult CancelBooking(string id)
        {
            var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            var trip = DataStore.Trips.FirstOrDefault(t => t.Id == booking.Trip.Id);
            if (trip != null)
            {
                trip.Passengers.Remove(booking.Passenger);
                trip.AvailableSeats++;
            }

            DataStore.Bookings.Remove(booking);
            return Ok();
        }

        [HttpPost("review")]
        public IActionResult LeaveReview([FromBody] Review review)
        {
            if (review.Rating < 1 || review.Rating > 5)
                return BadRequest("Недійсна оцінка");

            return Ok(review);
        }
    }
}
