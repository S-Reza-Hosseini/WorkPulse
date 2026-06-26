namespace WorkPulse.Services.Teams.Contracts.DTOs.Responses;

public class GetAllTeamsDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    public List<string> UserNames { get; set; } = [];
}