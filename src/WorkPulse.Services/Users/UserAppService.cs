using WorkPulse.Entities.TeamMemberships;
using WorkPulse.Entities.Users;
using WorkPulse.Services.Identity;
using WorkPulse.Services.TeamMembershipServices.Contracts.DTOs.Request;
using WorkPulse.Services.UnitOfWorks;
using WorkPulse.Services.Users.Contracts;
using WorkPulse.Services.Users.Contracts.DTOs.Request;
using WorkPulse.Services.Users.Contracts.DTOs.Response;
using WorkPulse.Services.Users.Exceptions;

namespace WorkPulse.Services.Users;

public class UserAppService(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IIdentityService identityService) : IUserService
{
    public async Task<BaseUserInformationDto> Add(AddUserDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Password = identityService.HashPassword(dto.Password),
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

        return new BaseUserInformationDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role
        };
    }

    public async Task Update(string userId, UpdateUserDto dto)
    {
        
        var user = await repository.Find(userId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        if (await repository.IsDuplicate(userId,dto.Username,dto.Email))
        {
            throw new DuplicateUserException();
        }
        
        user.Username = dto.Username;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        user.Avatar = dto.Avatar;
        user.Role = dto.Role;
        user.UpdatedAt = DateTime.UtcNow;


        AddTeamMemberships(user, dto.AddTeamMembershipDtos);
        DeleteTeamMemberships(user , dto.DeletedMembershipIds);
        
        await unitOfWork.Save();
    }

    public async Task<FindUserResponseDto> FindByUsername(string username)
    {
        var user = await repository.FindByUsername(username);

        return new FindUserResponseDto
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            IsExist = user != null,
            Password = user.Password
        };
    }

    public async Task<bool> CheckExistByUsername(string username)
    {
        return await repository.IsExistByUsername(username);
    }

    private void DeleteTeamMemberships(User user,
        List<long> dtoDeletedMembershipIds)
    {
        user.TeamMemberships
            .RemoveAll(t => dtoDeletedMembershipIds
                .Any(d => d == t.TeamId));
    }

    private void AddTeamMemberships(User user, 
        List<AddUserTeamMembershipDto> dtoAddTeamMembershipDtos)
    {
        foreach (var teamMembershipDto in dtoAddTeamMembershipDtos)
        {
            user.TeamMemberships.Add(new TeamMembership
            {
                TeamId = teamMembershipDto.TeamId,
                UserId = user.Id,
                JoinedAt = DateTime.UtcNow,
                Role = teamMembershipDto.TeamRole
            });
        }
    }
}