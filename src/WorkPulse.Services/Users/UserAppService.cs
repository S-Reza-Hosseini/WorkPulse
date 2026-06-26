using WorkPulse.Entities.Users;
using WorkPulse.Services.Common.Interfaces.identity;
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
        if (await repository.IsExistByUsername(dto.Username) || await repository.IsExistByEmail(dto.Email))
        {
            throw new DuplicateUserException();
        }

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
            Avatar = dto.Avatar
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

        await unitOfWork.Save();
    }

    public async Task Delete(string userId)
    {
        var user = await repository.Find(userId) ?? throw new UserNotFoundException();

        repository.Delete(user);
        await unitOfWork.Save();
    }

    public async Task<FindUserResponseDto> FindByUsername(string username)
    {
        var user = await repository.FindByUsername(username);

        if (user is null)
        {
            return new FindUserResponseDto { IsExist = false };
        }

        return new FindUserResponseDto
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            IsExist = true,
            Password = user.Password
        };
    }
}
