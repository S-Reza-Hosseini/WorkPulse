using WorkPulse.Contracts;
using WorkPulse.Entities.TeamMemberships.TeamRoles;
using WorkPulse.Services.TeamMemberships.Contracts.DTOs.Requests;

namespace WorkPulse.Services.TeamMemberships.Contracts;

public interface ITeamMembershipService : Service
{
    Task AddMember(string userId, bool isAdmin, long teamId, AddTeamMemberDto dto);
    Task RemoveMember(string userId, bool isAdmin, long teamId, int membershipId);
    Task ChangeRole(string userId, bool isAdmin, long teamId, int membershipId, TeamRole role);
}
