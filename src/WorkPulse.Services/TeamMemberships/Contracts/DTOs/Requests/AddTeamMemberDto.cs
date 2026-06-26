using WorkPulse.Entities.TeamMemberships.TeamRoles;

namespace WorkPulse.Services.TeamMemberships.Contracts.DTOs.Requests;

public class AddTeamMemberDto
{
    public required string UserId { get; set; }
    public TeamRole Role { get; set; }
}
