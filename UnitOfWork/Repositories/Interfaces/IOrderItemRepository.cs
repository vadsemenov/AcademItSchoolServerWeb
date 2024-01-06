using UnitOfWork.Model;

namespace UnitOfWork.Repositories.Interfaces;

public interface IOrderItemRepository : IMainRepository<OrderItem>
{
    List<Product> GetMostFrequentlyPurchasedProducts();
}