namespace WorkPulse.Services.Teams.Contracts.DTOs.Requests;

public class UpdateTeamDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
}
