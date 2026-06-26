using WorkPulse.Contracts;
using WorkPulse.Services.Teams.Contracts.DTOs.Responses;

namespace WorkPulse.Services.Teams.Contracts;

public interface ITeamQuery : Query
{
    Task<List<GetAllTeamsDto>> GetAll();
    Task<GetTeamDto?> GetById(string userId, bool isAdmin, long id);
}
