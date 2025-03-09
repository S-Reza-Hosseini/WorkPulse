using WorkPulse.Services.Users.Contracts.DTOs.Request;

namespace WorkPulse.Services.Users.Contracts;

public interface IUserService
{
    Task Add(AddUserDto dto);
}