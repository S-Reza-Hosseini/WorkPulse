namespace WorkPulse.Services.Authentications.Contracts.DTOs.Responses;

public class AuthResponseDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
