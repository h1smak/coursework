using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private static List<Trip> trips = new();

        [HttpGet]
        public IActionResult GetAll() => Ok(trips);

        [HttpPost]
        public IActionResult Create([FromBody] Trip trip)
        {
            if (trip.Date < DateTime.Now)
                return BadRequest("Дата в минулому");

            trips.Add(trip);
            return Ok(trip);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, [FromBody] Trip updated)
        {
            var trip = trips.FirstOrDefault(t => t.Id == id);
            if (trip == null) return NotFound();

            trip.From = updated.From;
            trip.To = updated.To;
            trip.Date = updated.Date;
            trip.DepartureTime = updated.DepartureTime;
            trip.Seats = updated.Seats;
            trip.AvailableSeats = updated.AvailableSeats;
            return Ok(trip);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var trip = trips.FirstOrDefault(t => t.Id == id);
            if (trip == null || trip.Passengers.Count > 0)
                return BadRequest("Неможливо скасувати поїздку");

            trips.Remove(trip);
            return Ok();
        }
    }
}
