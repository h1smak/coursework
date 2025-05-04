using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengerController : ControllerBase
    {
        private static List<Booking> bookings = new();

        [HttpPost("book")]
        public IActionResult BookPlace([FromBody] Booking booking)
        {
            if (booking == null || booking.Trip.AvailableSeats <= 0)
                return BadRequest("Місць немає");

            booking.Status = BookingStatus.Pending;
            bookings.Add(booking);
            booking.Trip.AvailableSeats--;
            return Ok(booking);
        }

        [HttpPost("cancel/{id}")]
        public IActionResult CancelBooking(string id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            if (!booking.Cancel(DateTime.Now))
                return BadRequest("Скасування можливе лише за 2 години до поїздки");

            bookings.Remove(booking);
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
