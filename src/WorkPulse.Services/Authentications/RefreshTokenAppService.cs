using WorkPulse.Entities.Authentications.RefreshTokens;
using WorkPulse.Services.Authentications.Contracts;
using WorkPulse.Services.Authentications.Contracts.DTOs.Responses;
using WorkPulse.Services.Authentications.Exceptions;
using WorkPulse.Services.Common.Interfaces.Security;
using WorkPulse.Services.UnitOfWorks;

namespace WorkPulse.Services.Authentications;

public class RefreshTokenAppService(
    IRefreshTokenRepository repository,
    ITokenService tokenService,
    IUnitOfWork unitOfWork) : IRefreshTokenService
{
    private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(7);

    public async Task<string> Issue(string userId)
    {
        var token = await CreateToken(userId);
        await unitOfWork.Save();

        return token;
    }

    public async Task<RefreshResultDto> Rotate(string refreshToken)
    {
        var existing = await repository.FindActiveByToken(refreshToken)
                       ?? throw new InvalidRefreshTokenException();

        existing.RevokedAt = DateTime.UtcNow;

        var newToken = await CreateToken(existing.UserId);
        await unitOfWork.Save();

        return new RefreshResultDto
        {
            UserId = existing.UserId,
            RefreshToken = newToken
        };
    }

    private async Task<string> CreateToken(string userId)
    {
        var token = tokenService.GenerateRefreshToken();

        await repository.Add(new RefreshToken
        {
            Token = token,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.Add(RefreshTokenLifetime)
        });

        return token;
    }
}
