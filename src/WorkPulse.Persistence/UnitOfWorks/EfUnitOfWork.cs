using WorkPulse.Persistence.DataContext;
using WorkPulse.Services.UnitOfWorks;

namespace WorkPulse.Persistence.UnitOfWorks;

public class EfUnitOfWork(EfDataContext context) : IUnitOfWork
{
    public async Task Save()
    {
        context.SaveChanges();
    }
    
    public async Task Begin()
    {
        context.Database.BeginTransaction();
    }

    public async Task Commit()
    {
        context.Database.CommitTransaction();
    }

    public async Task Rollback()
    {
        context.Database.RollbackTransaction();
    }
}