using WorkPulse.Entities.Users.UserRoles;

namespace WorkPulse.Services.Users.Contracts.DTOs.Request;

public class AddUserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Avatar { get; set; }
    public UserRole Role { get; set; }
}