using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Repositories;

public class ProductRepository : BaseEfRepository<Product>, IProductRepository
{
    public ProductRepository(DbContext dbContext) : base(dbContext)
    {
    }
}