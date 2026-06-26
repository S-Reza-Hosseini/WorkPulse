using WorkPulse.Entities.TeamMemberships.Permissions;

namespace WorkPulse.Entities.TeamMemberships.TeamRoles;

public static class TeamRolePermissionsExtensions
{
    private static readonly Dictionary<TeamRole, Permission[]> RolePermissions = new()
    {
        [TeamRole.ScrumMaster] = [Permission.ViewTask, Permission.CreateTask, Permission.EditTask, Permission.DeleteTask],
        [TeamRole.TeamMember] = [Permission.ViewTask, Permission.CreateTask, Permission.EditTask],
        [TeamRole.Viewer] = [Permission.ViewTask]
    };

    public static bool HasPermission(this TeamRole role, Permission permission) =>
        RolePermissions.TryGetValue(role, out var permissions) && permissions.Contains(permission);
}
