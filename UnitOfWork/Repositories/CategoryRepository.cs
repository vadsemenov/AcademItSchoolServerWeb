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
        var categoriesProductsCount = DbSet
            .Select(c => new
            {
                c.Name,
                ProductsCount = c.Products.SelectMany(p => p.OrderItems).Select(oi => oi.Count).Sum()
            })
            .ToDictionary(k => k.Name, v => v.ProductsCount);

        return categoriesProductsCount;
    }
}