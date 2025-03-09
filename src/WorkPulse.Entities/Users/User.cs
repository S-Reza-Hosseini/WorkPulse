using WorkPulse.Entities.TeamMemberships;
using WorkPulse.Entities.Users.UserRoles;

namespace WorkPulse.Entities.Users;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string UserName { get; set; } 
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Avatar { get; set; }
    public UserRole Role { get; set; }
    
    public List<TeamMembership> TeamMemberships { get; set; } = [];
}
