using WorkPulse.Entities.PlannerItems;
using WorkPulse.Services.PlannerItems.Contracts;
using WorkPulse.Services.PlannerItems.Contracts.DTOs.Requests;
using WorkPulse.Services.PlannerItems.Exceptions;
using WorkPulse.Services.UnitOfWorks;
using TaskStatus = WorkPulse.Entities.TaskItems.TaskStatuses.TaskStatus;

namespace WorkPulse.Services.PlannerItems;

public class PlannerItemAppService(
    IPlannerItemRepository repository,
    IUnitOfWork unitOfWork) : IPlannerItemService
{
    public async Task Add(string userId, AddPlannerItemDto dto)
    {
        var item = new PlannerItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await repository.Add(item);
        await unitOfWork.Save();
    }

    public async Task Update(string userId, long id, UpdatePlannerItemDto dto)
    {
        var item = await Find(userId, id);

        item.Title = dto.Title;
        item.Description = dto.Description;
        item.Priority = dto.Priority;
        item.DueDate = dto.DueDate;
        item.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.Save();
    }

    public async Task ChangeStatus(string userId, long id, TaskStatus status)
    {
        var item = await Find(userId, id);

        item.Status = status;
        item.CompletedAt = status == TaskStatus.Done ? DateTime.UtcNow : null;
        item.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.Save();
    }

    public async Task Delete(string userId, long id)
    {
        var item = await Find(userId, id);

        repository.Delete(item);
        await unitOfWork.Save();
    }

    private async Task<PlannerItem> Find(string userId, long id)
    {
        return await repository.Find(id, userId) ?? throw new PlannerItemNotFoundException();
    }
}
