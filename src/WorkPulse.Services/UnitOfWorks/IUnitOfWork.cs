namespace WorkPulse.Services.UnitOfWorks;

public interface IUnitOfWork
{
    Task Save();
    Task Begin();
    Task Commit();
    Task Rollback();
}