using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.PlannerItems;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.PlannerItems.Contracts;
using WorkPulse.Services.PlannerItems.Contracts.DTOs.Responses;

namespace WorkPulse.Persistence.PlannerItems;

public class PlannerItemQuery(EfDataContext dataContext) : IPlannerItemQuery
{
    private static readonly Expression<Func<PlannerItem, GetPlannerItemDto>> Projection = p => new GetPlannerItemDto
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        Status = p.Status,
        Priority = p.Priority,
        DueDate = p.DueDate,
        CreatedAt = p.CreatedAt,
        UpdatedAt = p.UpdatedAt,
        CompletedAt = p.CompletedAt
    };

    public async Task<List<GetPlannerItemDto>> GetAll(string userId)
    {
        return await dataContext.Set<PlannerItem>()
            .Where(p => p.UserId == userId)
            .Select(Projection)
            .ToListAsync();
    }

    public async Task<GetPlannerItemDto?> GetById(long id, string userId)
    {
        return await dataContext.Set<PlannerItem>()
            .Where(p => p.Id == id && p.UserId == userId)
            .Select(Projection)
            .FirstOrDefaultAsync();
    }
}
