using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private static List<Location> locations = new();

        [HttpPost]
        public IActionResult Add([FromBody] Location loc)
        {
            if (!Location.IsValid(loc.Name, loc.Latitude, loc.Longitude))
                return BadRequest("Некоректні дані");

            locations.Add(loc);
            return Ok(loc);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(locations);
    }
}
