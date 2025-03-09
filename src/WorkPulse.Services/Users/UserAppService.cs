using WorkPulse.Entities.TeamMemberships;
using WorkPulse.Entities.Users;
using WorkPulse.Services.UnitOfWorks;
using WorkPulse.Services.Users.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;

namespace WorkPulse.Services.Users;

public class UserAppService(
    IUserRepository repository,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task Add(AddUserDto dto)
    {
        var user = new User
        {
            UserName = dto.UserName,
            Password = dto.Password,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role,
            CreationDate = DateTime.UtcNow,
            Avatar = dto.Avatar,
            TeamMemberships = dto.TeamMemberships.Select(tm =>
                new TeamMembership
                {
                    TeamId = tm.TeamId,
                    JoinedAt = DateTime.UtcNow,
                    Role = tm.TeamRole,
                }).ToList()
        };
        
        await repository.Add(user);
        await unitOfWork.Save();
    }
}