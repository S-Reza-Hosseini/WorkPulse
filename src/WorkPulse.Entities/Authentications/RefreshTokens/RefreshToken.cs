using WorkPulse.Entities.Users;

namespace WorkPulse.Entities.Authentications.RefreshTokens;

public class RefreshToken
{
    public long Id { get; set; }
    public required string Token { get; set; }
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
}
