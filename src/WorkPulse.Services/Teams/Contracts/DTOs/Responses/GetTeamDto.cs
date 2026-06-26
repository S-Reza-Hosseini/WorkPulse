namespace WorkPulse.Services.Teams.Contracts.DTOs.Responses;

public class GetTeamDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    public List<TeamMemberDto> Members { get; set; } = [];
}
