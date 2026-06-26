using WorkPulse.Contracts;
using WorkPulse.Services.TaskItems.Contracts.DTOs.Requests;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Services.TaskItems.Contracts;

public interface ITaskItemService : Service
{
    Task Add(string userId, AddTaskItemDto dto);
    Task Update(string userId, long id, UpdateTaskItemDto dto);
    Task ChangeStatus(string userId, long id, TaskStatus status);
    Task Delete(string userId, long id);
}
