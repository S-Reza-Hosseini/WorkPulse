using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> IsDuplicate(string userId, string username, string email)
    {
        return await context.Set<User>()
            .AnyAsync(u => 
                (u.Username == username && u.Id != userId) ||
                (u.Email == email && u.Id != userId));
    }

    public async Task<User?> Find(string userId)
    {
        return await context.Set<User>()
            .Include(u => u.TeamMemberships)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> FindByUsername(string username)
    {
        return await context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> IsExistByUsername(string username)
    {
        return await context.Set<User>()
            .AnyAsync(u => u.Username == username);
    }

    public async Task<bool> IsExistByEmail(string email)
    {
        return await context.Set<User>()
            .AnyAsync(u => u.Email == email);
    }

    public void Delete(User user)
    {
        context.Set<User>().Remove(user);
    }
}