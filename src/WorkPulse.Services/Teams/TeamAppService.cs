using WorkPulse.Entities.TeamMemberships;
using WorkPulse.Entities.TeamMemberships.TeamRoles;
using WorkPulse.Entities.Teams;
using WorkPulse.Services.Teams.Contracts;
using WorkPulse.Services.Teams.Contracts.DTOs.Requests;
using WorkPulse.Services.Teams.Exceptions;
using WorkPulse.Services.TeamMemberships.Contracts;
using WorkPulse.Services.UnitOfWorks;

namespace WorkPulse.Services.Teams;

public class TeamAppService(
    ITeamRepository repository,
    ITeamMembershipQuery teamMembershipQuery,
    IUnitOfWork unitOfWork): ITeamService
{
    public async Task Add(AddTeamDto dto)
    {
        var team = new Team
        {
            Name = dto.Name,
            Description = dto.Description,
            Avatar = dto.Avatar,
            TeamMemberships = dto.Members.Select(m => new TeamMembership
            {
                UserId = m.UserId,
                Role = m.Role,
                JoinedAt = DateTime.UtcNow
            }).ToList()
        };
        await repository.Add(team);
        await unitOfWork.Save();
    }

    public async Task Update(string userId, bool isAdmin, long id, UpdateTeamDto dto)
    {
        var team = await repository.Find(id) ?? throw new TeamNotFoundException();
        await EnsureCanManageTeam(userId, isAdmin, id);

        team.Name = dto.Name;
        team.Description = dto.Description;
        team.Avatar = dto.Avatar;

        await unitOfWork.Save();
    }

    public async Task Delete(string userId, bool isAdmin, long id)
    {
        var team = await repository.Find(id) ?? throw new TeamNotFoundException();
        await EnsureCanManageTeam(userId, isAdmin, id);

        repository.Delete(team);
        await unitOfWork.Save();
    }

    private async Task EnsureCanManageTeam(string userId, bool isAdmin, long teamId)
    {
        if (isAdmin)
        {
            return;
        }

        var role = await teamMembershipQuery.GetRole(userId, teamId);
        if (role != TeamRole.ScrumMaster)
        {
            throw new InsufficientTeamPermissionException();
        }
    }
}
