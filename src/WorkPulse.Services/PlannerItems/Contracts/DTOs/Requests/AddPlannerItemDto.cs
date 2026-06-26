using WorkPulse.Entities.TaskItems.TaskPriorities;

namespace WorkPulse.Services.PlannerItems.Contracts.DTOs.Requests;

public class AddPlannerItemDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime DueDate { get; set; }
}
