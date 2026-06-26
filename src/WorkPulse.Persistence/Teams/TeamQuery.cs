using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.Teams;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.Teams.Contracts;
using WorkPulse.Services.Teams.Contracts.DTOs.Responses;
using WorkPulse.Services.Teams.Exceptions;

namespace WorkPulse.Persistence.Teams;

public class TeamQuery(EfDataContext dataContext) : ITeamQuery
{
    public async Task<List<GetAllTeamsDto>> GetAll()
    {
        return await (
            from team in dataContext.Set<Team>()
                .Include(t => t.TeamMemberships)
            select new GetAllTeamsDto
            {
                Name = team.Name,
                Description = team.Description,
                Avatar = team.Avatar,
                UserNames = team.TeamMemberships.Select(tm => tm.UserId).ToList()
            }).ToListAsync();
    }

    public async Task<GetTeamDto?> GetById(string userId, bool isAdmin, long id)
    {
        var team = await dataContext.Set<Team>()
            .Include(t => t.TeamMemberships)
            .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (team is null)
        {
            return null;
        }

        if (!isAdmin && team.TeamMemberships.All(m => m.UserId != userId))
        {
            throw new InsufficientTeamPermissionException();
        }

        return new GetTeamDto
        {
            Id = team.Id,
            Name = team.Name,
            Description = team.Description,
            Avatar = team.Avatar,
            Members = team.TeamMemberships.Select(m => new TeamMemberDto
            {
                MembershipId = m.Id,
                UserId = m.UserId,
                Username = m.User.Username,
                Role = m.Role,
                JoinedAt = m.JoinedAt
            }).ToList()
        };
    }
}
