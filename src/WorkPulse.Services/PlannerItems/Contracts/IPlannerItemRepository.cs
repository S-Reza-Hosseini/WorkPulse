using WorkPulse.Contracts;
using WorkPulse.Entities.PlannerItems;

namespace WorkPulse.Services.PlannerItems.Contracts;

public interface IPlannerItemRepository : Repository
{
    Task Add(PlannerItem item);
    Task<PlannerItem?> Find(long id, string userId);
    void Delete(PlannerItem item);
}
