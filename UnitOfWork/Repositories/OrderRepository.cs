using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Repositories;

public class OrderRepository : BaseEfRepository<Order>, IOrderRepository
{
    public OrderRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public Dictionary<Customer, decimal> GetClientsMaxSpentMoney()
    {
        return DbSet
             .GroupBy(o => o.Customer)
             .Select(g => new
             {
                 Customer = g.Key,
                 SpenMoney = g.SelectMany(o => o.OrderItems).Sum(oi => oi.Product.Price)
             })
             .ToDictionary(k => k.Customer, v => v.SpenMoney);
    }
}