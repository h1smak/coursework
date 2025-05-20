using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(DataStore.Bookings);
        }

        [HttpPost("confirm/{id}")]
        public IActionResult Confirm(string id)
        {
            var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            booking.Status = BookingStatus.Confirmed;
            return Ok(booking);
        }

        [HttpPost("cancel/{id}")]
        public IActionResult Cancel(string id)
        {
            var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();

            if (!booking.Cancel(DateTime.Now))
                return BadRequest("Пізно скасовувати");

            DataStore.Bookings.Remove(booking);
            return Ok();
        }
    }
}
