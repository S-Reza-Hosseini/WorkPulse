using System.Reflection.Metadata.Ecma335;

namespace WorkPulse.Services.Teams.Contracts.DTOs.Requests;

public class AddTeamDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    
}

