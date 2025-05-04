using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private static List<Payment> payments = new();

        [HttpPost]
        public IActionResult Pay([FromBody] Payment payment)
        {
            if (payment.Amount <= 0) return BadRequest("—ума маЇ бути б≥льше 0");

            payment.Status = PaymentStatus.Completed;
            payments.Add(payment);
            return Ok(payment);
        }
    }
}
