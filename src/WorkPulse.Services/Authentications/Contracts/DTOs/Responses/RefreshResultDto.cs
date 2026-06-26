namespace WorkPulse.Services.Authentications.Contracts.DTOs.Responses;

public class RefreshResultDto
{
    public required string UserId { get; set; }
    public required string RefreshToken { get; set; }
}
