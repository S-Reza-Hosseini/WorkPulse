using WorkPulse.Entities.TeamMemberships.Permissions;
using WorkPulse.Entities.TeamMemberships.TeamRoles;
using WorkPulse.Entities.Teams;
using WorkPulse.Entities.Users;

namespace WorkPulse.Entities.TeamMemberships;

public class TeamMembership
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public long? TeamId { get; set; }
    public Team? Team { get; set; } 
    public TeamRole Role { get; set; }
    public DateTime JoinedAt { get; set; }
    
    public List<Permission> Permissions { get; set; } = [];
}


