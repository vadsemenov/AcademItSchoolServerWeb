using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Repositories;

public class CustomerRepository : BaseEfRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(DbContext dbContext) : base(dbContext)
    {
    }
}