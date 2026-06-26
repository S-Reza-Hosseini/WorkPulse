using WorkPulse.Entities.TaskItems.TaskPriorities;

namespace WorkPulse.Services.TaskItems.Contracts.DTOs.Requests;

public class AddTaskItemDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime DueDate { get; set; }
    public double EstimatedTime { get; set; }
    public long TeamId { get; set; }
    public required string ActorId { get; set; }
}
