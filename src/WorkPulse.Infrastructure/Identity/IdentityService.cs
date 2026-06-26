using WorkPulse.Services.Common.Interfaces.identity;

namespace WorkPulse.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}