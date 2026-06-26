using WorkPulse.Contracts;
using WorkPulse.Entities.TeamMemberships;

namespace WorkPulse.Services.TeamMemberships.Contracts;

public interface ITeamMembershipRepository : Repository
{
    Task Add(TeamMembership membership);
    Task<TeamMembership?> Find(int id);
    Task<bool> IsMember(string userId, long teamId);
    void Delete(TeamMembership membership);
}
