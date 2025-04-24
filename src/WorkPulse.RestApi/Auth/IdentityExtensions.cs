namespace WorkPulse.RestApi.Auth;

public static class IdentityExtensions
{
    public static string GetUserId(this HttpContext context)
    {
        return context.User.Claims
            .FirstOrDefault(x =>
                string.Equals(x.Type,
                    "userid", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
    }
    
}