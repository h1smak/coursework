using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private static List<Booking> bookings = new();

        [HttpPost("confirm/{id}")]
        public IActionResult Confirm(string id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            booking.Status = BookingStatus.Confirmed;
            return Ok(booking);
        }

        [HttpPost("cancel/{id}")]
        public IActionResult Cancel(string id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            if (!booking.Cancel(DateTime.Now))
                return BadRequest("Пізно скасовувати");

            bookings.Remove(booking);
            return Ok();
        }
    }
}
