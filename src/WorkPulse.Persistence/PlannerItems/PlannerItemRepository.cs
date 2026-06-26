using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.PlannerItems;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.PlannerItems.Contracts;

namespace WorkPulse.Persistence.PlannerItems;

public class PlannerItemRepository(EfDataContext context) : IPlannerItemRepository
{
    public async Task Add(PlannerItem item)
    {
        await context.Set<PlannerItem>().AddAsync(item);
    }

    public async Task<PlannerItem?> Find(long id, string userId)
    {
        return await context.Set<PlannerItem>()
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
    }

    public void Delete(PlannerItem item)
    {
        context.Set<PlannerItem>().Remove(item);
    }
}
