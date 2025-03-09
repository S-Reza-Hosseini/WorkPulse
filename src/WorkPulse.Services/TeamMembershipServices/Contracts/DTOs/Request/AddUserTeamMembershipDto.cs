using WorkPulse.Entities.TeamMemberships.TeamRoles;

namespace WorkPulse.Services.TeamMembershipServices.Contracts.DTOs.Request;

public class AddUserTeamMembershipDto
{
    public long TeamId { get; set; }
    public TeamRole TeamRole { get; set; }
}