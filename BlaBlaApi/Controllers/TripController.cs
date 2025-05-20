using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(DataStore.Trips);

        [HttpPost]
        public IActionResult Create([FromBody] Trip trip)
        {
            if (trip.Date < DateTime.Now)
                return BadRequest("Дата в минулому");

            DataStore.Trips.Add(trip);
            return Ok(trip);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, [FromBody] Trip updated)
        {
            var trip = DataStore.Trips.FirstOrDefault(t => t.Id == id);
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
            var trip = DataStore.Trips.FirstOrDefault(t => t.Id == id);
            if (trip == null || trip.Passengers.Count > 0)
                return BadRequest("Неможливо скасувати поїздку");

            DataStore.Trips.Remove(trip);
            return Ok();
        }
    }
}
