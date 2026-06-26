using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.UnitOfWorks;

namespace WorkPulse.Persistence.UnitOfWorks;

public class EfUnitOfWork(EfDataContext context) : IUnitOfWork
{
    public async Task Save()
    {
        await context.SaveChangesAsync();
    }

    public async Task Begin()
    {
        await context.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await context.Database.CommitTransactionAsync();
    }

    public async Task Rollback()
    {
        await context.Database.RollbackTransactionAsync();
    }
}