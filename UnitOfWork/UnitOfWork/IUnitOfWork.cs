namespace UnitOfWork.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void Save();

    T GetRepository<T>() where T : class;
    void Dispose();
}