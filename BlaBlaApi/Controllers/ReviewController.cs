using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private static List<Review> reviews = new();

        [HttpPost]
        public IActionResult Add([FromBody] Review review)
        {
            if (review.Rating < 1 || review.Rating > 5)
                return BadRequest("������ �� ���� �� 1 �� 5");

            reviews.Add(review);
            return Ok(review);
        }
    }
}
