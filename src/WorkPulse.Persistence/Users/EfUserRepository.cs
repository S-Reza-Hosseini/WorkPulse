using WorkPulse.Entities.Users;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.Users.Contracts;

namespace WorkPulse.Persistence.Users;

public class EfUserRepository(EfDataContext context) : IUserRepository
{
    public async Task Add(User user)
    {
        await context.Set<User>().AddAsync(user);
    }
}