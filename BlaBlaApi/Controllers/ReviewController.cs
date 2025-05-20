using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add([FromBody] Review review)
        {
            if (review.Rating < 1 || review.Rating > 5)
                return BadRequest("������ �� ���� �� 1 �� 5");

            DataStore.reviews.Add(review);
            return Ok(review);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(DataStore.reviews);
        }

        [HttpGet("driver/{id}")]
        public IActionResult GetReviewsByDriverId(string id)
        {
            var reviews = DataStore.reviews.Where(r => r.Target.Id == id).ToList();

            if (!reviews.Any())
                return NotFound("³����� ��� ����� ���� �� ��������");

            return Ok(reviews);
        }
    }
}

