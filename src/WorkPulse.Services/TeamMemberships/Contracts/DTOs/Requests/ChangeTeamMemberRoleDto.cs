using WorkPulse.Entities.TeamMemberships.TeamRoles;

namespace WorkPulse.Services.TeamMemberships.Contracts.DTOs.Requests;

public class ChangeTeamMemberRoleDto
{
    public TeamRole Role { get; set; }
}
