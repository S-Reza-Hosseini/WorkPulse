using WorkPulse.Contracts;
using WorkPulse.Services.PlannerItems.Contracts.DTOs.Requests;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Services.PlannerItems.Contracts;

public interface IPlannerItemService : Service
{
    Task Add(string userId, AddPlannerItemDto dto);
    Task Update(string userId, long id, UpdatePlannerItemDto dto);
    Task ChangeStatus(string userId, long id, TaskStatus status);
    Task Delete(string userId, long id);
}
