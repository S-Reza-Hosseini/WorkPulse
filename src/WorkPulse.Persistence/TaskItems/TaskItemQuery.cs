using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.TaskItems;
using WorkPulse.Entities.TeamMemberships.Permissions;
using WorkPulse.Entities.TeamMemberships.TeamRoles;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.TaskItems.Contracts;
using WorkPulse.Services.TaskItems.Contracts.DTOs.Responses;
using WorkPulse.Services.TaskItems.Exceptions;
using WorkPulse.Services.TeamMemberships.Contracts;

namespace WorkPulse.Persistence.TaskItems;

public class TaskItemQuery(EfDataContext dataContext, ITeamMembershipQuery teamMembershipQuery) : ITaskItemQuery
{
    private static readonly Expression<Func<TaskItem, GetTaskItemDto>> Projection = t => new GetTaskItemDto
    {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        Status = t.Status,
        Priority = t.Priority,
        DueDate = t.DueDate,
        EstimatedTime = t.EstimatedTime,
        CreatorId = t.CreatorId,
        ActorId = t.ActorId,
        TeamId = t.TeamId,
        CreatedAt = t.CreatedAt,
        UpdatedAt = t.UpdatedAt,
        CompletedAt = t.CompletedAt
    };

    public async Task<List<GetTaskItemDto>> GetAll(string userId, long? teamId)
    {
        if (teamId.HasValue)
        {
            await EnsureCanView(userId, teamId.Value);

            return await dataContext.Set<TaskItem>()
                .Where(t => t.TeamId == teamId.Value)
                .Select(Projection)
                .ToListAsync();
        }

        var teamIds = await teamMembershipQuery.GetTeamIdsForUser(userId);

        return await dataContext.Set<TaskItem>()
            .Where(t => teamIds.Contains(t.TeamId))
            .Select(Projection)
            .ToListAsync();
    }

    public async Task<GetTaskItemDto?> GetById(string userId, long id)
    {
        var taskItem = await dataContext.Set<TaskItem>()
            .Where(t => t.Id == id)
            .Select(Projection)
            .FirstOrDefaultAsync();

        if (taskItem is null)
        {
            return null;
        }

        await EnsureCanView(userId, taskItem.TeamId);

        return taskItem;
    }

    private async Task EnsureCanView(string userId, long teamId)
    {
        var role = await teamMembershipQuery.GetRole(userId, teamId);
        if (role is null || !role.Value.HasPermission(Permission.ViewTask))
        {
            throw new InsufficientTaskPermissionException();
        }
    }
}
