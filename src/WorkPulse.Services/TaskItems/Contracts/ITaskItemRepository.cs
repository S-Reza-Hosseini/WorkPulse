using WorkPulse.Contracts;
using WorkPulse.Entities.TaskItems;

namespace WorkPulse.Services.TaskItems.Contracts;

public interface ITaskItemRepository : Repository
{
    Task Add(TaskItem taskItem);
    Task<TaskItem?> Find(long id);
    void Delete(TaskItem taskItem);
}
