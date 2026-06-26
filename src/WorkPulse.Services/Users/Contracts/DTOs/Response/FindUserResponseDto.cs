using WorkPulse.Entities.Users.UserRoles;

namespace WorkPulse.Services.Users.Contracts.DTOs.Response;

public class FindUserResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserRole Role { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public bool IsExist { get; set; }
}