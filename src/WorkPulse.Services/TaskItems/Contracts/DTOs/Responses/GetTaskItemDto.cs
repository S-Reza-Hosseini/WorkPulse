using WorkPulse.Entities.TaskItems.TaskPriorities;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Services.TaskItems.Contracts.DTOs.Responses;

public class GetTaskItemDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public double EstimatedTime { get; set; }
    public required string CreatorId { get; set; }
    public required string ActorId { get; set; }
    public long TeamId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
