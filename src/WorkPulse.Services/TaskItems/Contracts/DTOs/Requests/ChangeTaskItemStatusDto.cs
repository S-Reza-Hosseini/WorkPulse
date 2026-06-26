using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Services.TaskItems.Contracts.DTOs.Requests;

public class ChangeTaskItemStatusDto
{
    public TaskStatus Status { get; set; }
}
