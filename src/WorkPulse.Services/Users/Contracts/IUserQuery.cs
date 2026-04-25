using WorkPulse.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Response;

namespace WorkPulse.Services.Users.Contracts;

public interface IUserQuery : Query
{
    Task<List<GetAllUserDto>> GetAll();
    Task<GetUserDto?> GetById(string id);
}