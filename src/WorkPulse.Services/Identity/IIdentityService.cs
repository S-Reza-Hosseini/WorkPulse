using WorkPulse.Contracts;

namespace WorkPulse.Services.Identity;

public interface IIdentityService 
{
    string HashPassword(string password);
    bool  VerifyPassword(string hashedPassword, string providedPassword);
}