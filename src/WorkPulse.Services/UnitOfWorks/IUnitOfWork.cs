using WorkPulse.Contracts;

namespace WorkPulse.Services.UnitOfWorks;

public interface IUnitOfWork : Repository
{
    Task Save();
    Task Begin();
    Task Commit();
    Task Rollback();
}