using WorkPulse.Entities.TaskItems;
using WorkPulse.Entities.TeamMemberships;

namespace WorkPulse.Entities.Teams;

public class Team
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    
    public List<TeamMembership> TeamMemberships { get; set; } = [];
    public List<TaskItem> TaskItems { get; set; } = [];
}