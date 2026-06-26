namespace WorkPulse.Services.Common.Interfaces.identity;

public interface IIdentityService 
{
    string HashPassword(string password);
    bool  VerifyPassword(string hashedPassword, string providedPassword);
}