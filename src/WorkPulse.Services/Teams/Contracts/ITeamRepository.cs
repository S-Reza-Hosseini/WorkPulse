using WorkPulse.Contracts;
using WorkPulse.Entities.Teams;

namespace WorkPulse.Services.Teams.Contracts;

public interface ITeamRepository: Repository
{
    Task Add(Team team);
    Task<Team?> Find(long id);
    void Delete(Team team);
}
