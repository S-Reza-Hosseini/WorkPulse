using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.TeamMemberships;
using WorkPulse.Entities.TeamMemberships.TeamRoles;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.TeamMemberships.Contracts;

namespace WorkPulse.Persistence.TeamMemberships;

public class TeamMembershipQuery(EfDataContext dataContext) : ITeamMembershipQuery
{
    public async Task<TeamRole?> GetRole(string userId, long teamId)
    {
        return await dataContext.Set<TeamMembership>()
            .Where(tm => tm.UserId == userId && tm.TeamId == teamId)
            .Select(tm => (TeamRole?)tm.Role)
            .FirstOrDefaultAsync();
    }

    public async Task<List<long>> GetTeamIdsForUser(string userId)
    {
        return await dataContext.Set<TeamMembership>()
            .Where(tm => tm.UserId == userId && tm.TeamId != null)
            .Select(tm => tm.TeamId!.Value)
            .ToListAsync();
    }
}
