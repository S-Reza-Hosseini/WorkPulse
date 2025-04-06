using WorkPulse.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;

namespace WorkPulse.Services.Users.Contracts;

public interface IUserService : Service
{
    Task Add(AddUserDto dto);
    Task Update(string userId, UpdateUserDto dto);
}