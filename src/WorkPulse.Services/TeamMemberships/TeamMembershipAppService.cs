using WorkPulse.Entities.TeamMemberships;
using WorkPulse.Entities.TeamMemberships.TeamRoles;
using WorkPulse.Services.TeamMemberships.Contracts;
using WorkPulse.Services.TeamMemberships.Contracts.DTOs.Requests;
using WorkPulse.Services.Teams.Contracts;
using WorkPulse.Services.Teams.Exceptions;
using WorkPulse.Services.UnitOfWorks;

namespace WorkPulse.Services.TeamMemberships;

public class TeamMembershipAppService(
    ITeamMembershipRepository repository,
    ITeamMembershipQuery teamMembershipQuery,
    ITeamRepository teamRepository,
    IUnitOfWork unitOfWork) : ITeamMembershipService
{
    public async Task AddMember(string userId, bool isAdmin, long teamId, AddTeamMemberDto dto)
    {
        if (await teamRepository.Find(teamId) is null)
        {
            throw new TeamNotFoundException();
        }

        await EnsureCanManageTeam(userId, isAdmin, teamId);

        if (await repository.IsMember(dto.UserId, teamId))
        {
            throw new DuplicateTeamMembershipException();
        }

        var membership = new TeamMembership
        {
            UserId = dto.UserId,
            TeamId = teamId,
            Role = dto.Role,
            JoinedAt = DateTime.UtcNow
        };

        await repository.Add(membership);
        await unitOfWork.Save();
    }

    public async Task RemoveMember(string userId, bool isAdmin, long teamId, int membershipId)
    {
        await EnsureCanManageTeam(userId, isAdmin, teamId);

        var membership = await Find(teamId, membershipId);

        repository.Delete(membership);
        await unitOfWork.Save();
    }

    public async Task ChangeRole(string userId, bool isAdmin, long teamId, int membershipId, TeamRole role)
    {
        await EnsureCanManageTeam(userId, isAdmin, teamId);

        var membership = await Find(teamId, membershipId);
        membership.Role = role;

        await unitOfWork.Save();
    }

    private async Task<TeamMembership> Find(long teamId, int membershipId)
    {
        var membership = await repository.Find(membershipId);
        if (membership is null || membership.TeamId != teamId)
        {
            throw new TeamMembershipNotFoundException();
        }

        return membership;
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
