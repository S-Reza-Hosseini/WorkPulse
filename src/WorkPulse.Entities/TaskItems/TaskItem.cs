using WorkPulse.Entities.TaskItems.TaskPriorities;
using WorkPulse.Entities.Teams;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Entities.TaskItems;

public class TaskItem
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime DueDate { get; set; }
    public double EstimatedTime { get; set; }
    public string CreatorId { get; set; } = default!;
    public string ActorId { get; set; } = default!;
    public long TeamId { get; set; }
    public Team Team { get; set; } = default!;
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
