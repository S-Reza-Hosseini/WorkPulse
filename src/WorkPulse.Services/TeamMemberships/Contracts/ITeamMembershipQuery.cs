using WorkPulse.Contracts;
using WorkPulse.Entities.TeamMemberships.TeamRoles;

namespace WorkPulse.Services.TeamMemberships.Contracts;

public interface ITeamMembershipQuery : Query
{
    Task<TeamRole?> GetRole(string userId, long teamId);
    Task<List<long>> GetTeamIdsForUser(string userId);
}
