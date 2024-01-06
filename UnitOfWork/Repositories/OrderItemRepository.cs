using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Repositories;

public class OrderItemRepository : BaseEfRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public List<Product> GetMostFrequentlyPurchasedProducts()
    {
        var products = DbSet
            .GroupBy(o => o.Product)
            .Select(g => new
            {
                Product = g.Key,
                ProductCount = g.Sum(orderItem => orderItem.Count)
            })
            .Where(pg => pg.ProductCount == DbSet
                .GroupBy(oi => oi.Product)
                .Select(oig => oig.Sum(c => c.Count))
                .Max())
            .Select(pg => pg.Product)
            .ToList();

        return products;
    }
}