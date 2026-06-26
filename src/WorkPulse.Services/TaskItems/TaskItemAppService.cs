using WorkPulse.Entities.TaskItems;
using WorkPulse.Entities.TeamMemberships.Permissions;
using WorkPulse.Entities.TeamMemberships.TeamRoles;
using WorkPulse.Services.TaskItems.Contracts;
using WorkPulse.Services.TaskItems.Contracts.DTOs.Requests;
using WorkPulse.Services.TaskItems.Exceptions;
using WorkPulse.Services.TeamMemberships.Contracts;
using WorkPulse.Services.UnitOfWorks;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Services.TaskItems;

public class TaskItemAppService(
    ITaskItemRepository repository,
    ITeamMembershipQuery teamMembershipQuery,
    IUnitOfWork unitOfWork) : ITaskItemService
{
    public async Task Add(string userId, AddTaskItemDto dto)
    {
        await EnsurePermission(userId, dto.TeamId, Permission.CreateTask);

        var taskItem = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            EstimatedTime = dto.EstimatedTime,
            TeamId = dto.TeamId,
            CreatorId = userId,
            ActorId = dto.ActorId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await repository.Add(taskItem);
        await unitOfWork.Save();
    }

    public async Task Update(string userId, long id, UpdateTaskItemDto dto)
    {
        var taskItem = await Find(id);
        await EnsurePermission(userId, taskItem.TeamId, Permission.EditTask);

        taskItem.Title = dto.Title;
        taskItem.Description = dto.Description;
        taskItem.Priority = dto.Priority;
        taskItem.DueDate = dto.DueDate;
        taskItem.EstimatedTime = dto.EstimatedTime;
        taskItem.ActorId = dto.ActorId;
        taskItem.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.Save();
    }

    public async Task ChangeStatus(string userId, long id, TaskStatus status)
    {
        var taskItem = await Find(id);
        await EnsurePermission(userId, taskItem.TeamId, Permission.EditTask);

        taskItem.Status = status;
        taskItem.CompletedAt = status == TaskStatus.Done ? DateTime.UtcNow : null;
        taskItem.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.Save();
    }

    public async Task Delete(string userId, long id)
    {
        var taskItem = await Find(id);
        await EnsurePermission(userId, taskItem.TeamId, Permission.DeleteTask);

        repository.Delete(taskItem);
        await unitOfWork.Save();
    }

    private async Task<TaskItem> Find(long id)
    {
        return await repository.Find(id) ?? throw new TaskItemNotFoundException();
    }

    private async Task EnsurePermission(string userId, long teamId, Permission permission)
    {
        var role = await teamMembershipQuery.GetRole(userId, teamId);
        if (role is null || !role.Value.HasPermission(permission))
        {
            throw new InsufficientTaskPermissionException();
        }
    }
}
