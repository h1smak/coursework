using BlaBlaApi.DTOs;
using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static List<User> users = new();

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(users);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Email �� ������ ����'�����.");

            try
            {
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    BirthDate = userDto.BirthDate,
                    Rating = userDto.Rating,
                    Role = userDto.Role,
                    Password = userDto.Password
                };

                users.Add(user);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Email �� ������ ����'�����.");

            var user = users.FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
            return user != null ? Ok(user) : NotFound("������������ email ��� ������");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("�� ������ � ��������� ������.");
        }

        [HttpDelete("delete")]
        public IActionResult DeleteAccount([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Email �� ������ ����'�����.");

            var user = users.FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
            if (user == null)
                return NotFound("���������� �� �������� ��� ���������� ���.");

            users.Remove(user);
            return Ok("�������� ����� ������ ��������.");
        }

    }

}
