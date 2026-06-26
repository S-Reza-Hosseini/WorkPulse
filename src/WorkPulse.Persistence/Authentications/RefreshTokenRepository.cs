using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.Authentications.RefreshTokens;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.Authentications.Contracts;

namespace WorkPulse.Persistence.Authentications;

public class RefreshTokenRepository(EfDataContext context) : IRefreshTokenRepository
{
    public async Task Add(RefreshToken token)
    {
        await context.Set<RefreshToken>().AddAsync(token);
    }

    public async Task<RefreshToken?> FindActiveByToken(string token)
    {
        var now = DateTime.UtcNow;

        return await context.Set<RefreshToken>()
            .FirstOrDefaultAsync(t => t.Token == token && t.RevokedAt == null && t.ExpiresAt > now);
    }
}
