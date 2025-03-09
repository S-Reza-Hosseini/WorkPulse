using WorkPulse.Entities.Users;

namespace WorkPulse.Services.Users.Contracts;

public interface IUserRepository
{
    Task Add(User user);
}