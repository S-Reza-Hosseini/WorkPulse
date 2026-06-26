using WorkPulse.Contracts;
using WorkPulse.Services.TaskItems.Contracts.DTOs.Responses;

namespace WorkPulse.Services.TaskItems.Contracts;

public interface ITaskItemQuery : Query
{
    Task<List<GetTaskItemDto>> GetAll(string userId, long? teamId);
    Task<GetTaskItemDto?> GetById(string userId, long id);
}
