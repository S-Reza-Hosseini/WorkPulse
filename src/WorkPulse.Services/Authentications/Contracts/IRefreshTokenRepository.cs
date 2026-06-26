using WorkPulse.Contracts;
using WorkPulse.Entities.Authentications.RefreshTokens;

namespace WorkPulse.Services.Authentications.Contracts;

public interface IRefreshTokenRepository : Repository
{
    Task Add(RefreshToken token);
    Task<RefreshToken?> FindActiveByToken(string token);
}
