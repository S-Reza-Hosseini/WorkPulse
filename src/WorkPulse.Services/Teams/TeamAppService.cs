using WorkPulse.Entities.Teams;
using WorkPulse.Services.Teams.Contracts;
using WorkPulse.Services.Teams.Contracts.DTOs.Requests;
using WorkPulse.Services.UnitOfWorks;

namespace WorkPulse.Services.Teams;

public class TeamAppService(ITeamRepository repository,
                            IUnitOfWork unitOfWork): ITeamService
{
    public async Task Add(AddTeamDto dto)
    {
        var team = new Team
        {
            Name = dto.Name,
            Description = dto.Description,
            Avatar = dto.Avatar
        };
        await repository.Add(team);
        await unitOfWork.Save();
    }
}