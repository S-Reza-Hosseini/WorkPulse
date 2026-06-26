using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Services.PlannerItems.Contracts.DTOs.Requests;

public class ChangePlannerItemStatusDto
{
    public TaskStatus Status { get; set; }
}
