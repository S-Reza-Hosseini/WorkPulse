using WorkPulse.Entities.Users.UserRoles;

namespace WorkPulse.Services.Users.Contracts.DTOs.Response;

public class GetAllUserDto
{
    public required string Id { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public UserRole Role { get; set; }
}