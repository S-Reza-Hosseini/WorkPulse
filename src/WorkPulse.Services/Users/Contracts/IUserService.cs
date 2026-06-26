using WorkPulse.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;
using WorkPulse.Services.Users.Contracts.DTOs.Response;

namespace WorkPulse.Services.Users.Contracts;

public interface IUserService : Service
{
    Task<BaseUserInformationDto> Add(AddUserDto dto);
    Task Update(string userId, UpdateUserDto dto);
    Task Delete(string userId);
    Task<FindUserResponseDto> FindByUsername(string dtoUsername);
}