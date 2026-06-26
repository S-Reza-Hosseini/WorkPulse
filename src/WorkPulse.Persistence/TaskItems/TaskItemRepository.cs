using Microsoft.EntityFrameworkCore;
using WorkPulse.Entities.TaskItems;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.TaskItems.Contracts;

namespace WorkPulse.Persistence.TaskItems;

public class TaskItemRepository(EfDataContext context) : ITaskItemRepository
{
    public async Task Add(TaskItem taskItem)
    {
        await context.Set<TaskItem>().AddAsync(taskItem);
    }

    public async Task<TaskItem?> Find(long id)
    {
        return await context.Set<TaskItem>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public void Delete(TaskItem taskItem)
    {
        context.Set<TaskItem>().Remove(taskItem);
    }
}
