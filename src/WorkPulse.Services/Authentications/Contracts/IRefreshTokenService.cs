using WorkPulse.Contracts;
using WorkPulse.Services.Authentications.Contracts.DTOs.Responses;

namespace WorkPulse.Services.Authentications.Contracts;

public interface IRefreshTokenService : Service
{
    Task<string> Issue(string userId);
    Task<RefreshResultDto> Rotate(string refreshToken);
}
