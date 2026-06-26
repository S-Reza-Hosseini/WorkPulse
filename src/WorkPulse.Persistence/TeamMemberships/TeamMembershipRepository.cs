using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.TeamMemberships;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.TeamMemberships.Contracts;

namespace WorkPulse.Persistence.TeamMemberships;

public class TeamMembershipRepository(EfDataContext context) : ITeamMembershipRepository
{
    public async Task Add(TeamMembership membership)
    {
        await context.Set<TeamMembership>().AddAsync(membership);
    }

    public async Task<TeamMembership?> Find(int id)
    {
        return await context.Set<TeamMembership>().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> IsMember(string userId, long teamId)
    {
        return await context.Set<TeamMembership>()
            .AnyAsync(m => m.UserId == userId && m.TeamId == teamId);
    }

    public void Delete(TeamMembership membership)
    {
        context.Set<TeamMembership>().Remove(membership);
    }
}
