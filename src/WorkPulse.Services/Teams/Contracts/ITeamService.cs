using WorkPulse.Contracts;
using WorkPulse.Services.Teams.Contracts.DTOs.Requests;

namespace WorkPulse.Services.Teams.Contracts;

public interface ITeamService: Service
{
    Task Add(AddTeamDto dto);
    Task Update(string userId, bool isAdmin, long id, UpdateTeamDto dto);
    Task Delete(string userId, bool isAdmin, long id);
}
