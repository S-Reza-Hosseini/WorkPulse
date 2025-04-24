using WorkPulse.Entities.Users.UserRoles;

namespace WorkPulse.Services.Users.Contracts.DTOs.Response;

public class BaseUserInformationDto
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string PhoneNumber { get; set; }
    public required UserRole Role { get; set; }
}