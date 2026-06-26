using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.Teams;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.Teams.Contracts;

namespace WorkPulse.Persistence.Teams;

public class TeamRepository(EfDataContext context) : ITeamRepository
{
    public async Task Add(Team team)
    {
        await context.Set<Team>().AddAsync(team);
    }

    public async Task<Team?> Find(long id)
    {
        return await context.Set<Team>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public void Delete(Team team)
    {
        context.Set<Team>().Remove(team);
    }
}
