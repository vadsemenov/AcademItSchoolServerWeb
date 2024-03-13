using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UnitOfWork.Repositories;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    private IDbContextTransaction? _transaction;

    private bool _disposed;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new Exception("DbContext is null!");
    }

    public T GetRepository<T>() where T : class, IRepository
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (typeof(T) == typeof(ICategoryRepository))
        {
            return (new CategoryRepository(_dbContext) as T)!;
        }

        if (typeof(T) == typeof(ICustomerRepository))
        {
            return (new CustomerRepository(_dbContext) as T)!;
        }

        if (typeof(T) == typeof(IOrderItemRepository))
        {
            return (new OrderItemRepository(_dbContext) as T)!;
        }

        if (typeof(T) == typeof(IOrderRepository))
        {
            return (new OrderRepository(_dbContext) as T)!;
        }

        if (typeof(T) == typeof(IProductRepository))
        {
            return (new ProductRepository(_dbContext) as T)!;
        }

        throw new Exception($"Repository {typeof(T)} does not exist!");
    }

    public void BeginTransaction()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction != null)
        {
            throw new Exception("There is already an existing transaction!");
        }

        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction == null)
        {
            return;
        }

        _transaction.Commit();

        _transaction = null;
    }

    public void RollbackTransaction()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction == null)
        {
            return;
        }

        _transaction.Rollback();

        _transaction = null;
    }

    public void Save()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _disposed = true;

        _dbContext.Dispose();
    }
}