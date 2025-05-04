using BlaBlaApi.Models;

namespace BlaBlaApi.DTOs
{
    public class RegisterUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public double Rating { get; set; }
    public UserRole Role { get; set; }
    public string Password { get; set; } 
}

}
