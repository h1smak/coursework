using BlaBlaApi.Controllers;
using BlaBlaApi.DTOs;
using BlaBlaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlaBlaTest
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new UserController();
        }

        [TestMethod]
        public void Register_WithValidData_ReturnsOk()
        {
            var dto = new RegisterUserDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Phone = "1234567890",
                BirthDate = new DateTime(2000, 1, 1),
                Rating = 5,
                Role = UserRole.Passenger,
                Password = "password123"
            };

            var result = _controller.Register(dto) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var user = result.Value as User;
            Assert.AreEqual(dto.Email, user.Email);
        }

        [TestMethod]
        public void Register_WithMissingEmail_ReturnsBadRequest()
        {
            var dto = new RegisterUserDto
            {
                Email = "",
                Password = "password123"
            };

            var result = _controller.Register(dto) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void Login_WithValidCredentials_ReturnsOk()
        {
            var registerDto = new RegisterUserDto
            {
                Name = "User",
                Email = "user@example.com",
                Password = "pass",
                Phone = "1234567890",
                BirthDate = new DateTime(2000, 1, 1),
                Rating = 5,
                Role = UserRole.Passenger
            };

            _controller.Register(registerDto);

            var loginDto = new LoginDto
            {
                Email = "user@example.com",
                Password = "pass"
            };

            var result = _controller.Login(loginDto) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void Login_WithInvalidCredentials_ReturnsNotFound()
        {
            var loginDto = new LoginDto
            {
                Email = "wrong@example.com",
                Password = "wrong"
            };

            var result = _controller.Login(loginDto) as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void DeleteAccount_WithValidCredentials_ReturnsOk()
        {
            var dto = new RegisterUserDto
            {
                Name = "ToDelete",
                Email = "delete@example.com",
                Password = "1234",
                Phone = "1234567890",
                BirthDate = new DateTime(2000, 1, 1),
                Rating = 5,
                Role = UserRole.Passenger
            };

            _controller.Register(dto);

            var loginDto = new LoginDto
            {
                Email = "delete@example.com",
                Password = "1234"
            };

            var result = _controller.DeleteAccount(loginDto) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void DeleteAccount_WithInvalidCredentials_ReturnsNotFound()
        {
            var loginDto = new LoginDto
            {
                Email = "nonexistent@example.com",
                Password = "nopass"
            };

            var result = _controller.DeleteAccount(loginDto) as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void Logout_ReturnsOk()
        {
            var result = _controller.Logout() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Ви вийшли з облікового запису.", result.Value);
        }
    }
}