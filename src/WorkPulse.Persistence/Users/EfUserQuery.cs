using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.Users;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.Users.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Response;

namespace WorkPulse.Persistence.Users;

public class EfUserQuery(EfDataContext context) : IUserQuery
{
    public async Task<List<GetAllUserDto>> GetAll()
    {
        return await
            (from user in context.Set<User>()
                select new GetAllUserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role
                }).ToListAsync();
    }

    public async Task<GetUserDto?> GetById(string id)
    {
        return await (
            from user in context.Set<User>()
            where user.Id == id
                select new GetUserDto
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    Avatar = user.Avatar
                }).FirstOrDefaultAsync();
    }
}