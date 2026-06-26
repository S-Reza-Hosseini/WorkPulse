using WorkPulse.Entities.TeamMemberships.TeamRoles;

namespace WorkPulse.Services.Teams.Contracts.DTOs.Responses;

public class TeamMemberDto
{
    public int MembershipId { get; set; }
    public required string UserId { get; set; }
    public required string Username { get; set; }
    public TeamRole Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
