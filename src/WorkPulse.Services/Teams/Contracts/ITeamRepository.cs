using WorkPulse.Contracts;
using WorkPulse.Entities.Teams;
using WorkPulse.Services.Teams.Contracts.DTOs.Requests;

namespace WorkPulse.Services.Teams.Contracts;

public interface ITeamRepository: Repository
{
    Task Add(Team team);
}