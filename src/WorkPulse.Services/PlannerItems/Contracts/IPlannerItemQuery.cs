using WorkPulse.Contracts;
using WorkPulse.Services.PlannerItems.Contracts.DTOs.Responses;

namespace WorkPulse.Services.PlannerItems.Contracts;

public interface IPlannerItemQuery : Query
{
    Task<List<GetPlannerItemDto>> GetAll(string userId);
    Task<GetPlannerItemDto?> GetById(long id, string userId);
}
