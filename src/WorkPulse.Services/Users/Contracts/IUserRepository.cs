using WorkPulse.Contracts;
using WorkPulse.Entities.Users;

namespace WorkPulse.Services.Users.Contracts;

public interface IUserRepository : Repository
{
    Task Add(User user);
    Task<bool> IsDuplicate(string userId, string username, string email);
    Task<User?> Find(string userId);
}