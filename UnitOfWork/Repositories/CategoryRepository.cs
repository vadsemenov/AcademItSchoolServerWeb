using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Repositories;

public class CategoryRepository : BaseEfRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public Dictionary<string, int> GetCategoriesProductsCount()
    {
        return DbSet
            .Select(c => new
            {
                c.Name,
                ProductsCount = c.Products.SelectMany(p => p.OrderItems).Sum(oi => oi.Count)
            })
            .ToDictionary(k => k.Name, v => v.ProductsCount);
    }
}