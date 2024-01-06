using UnitOfWork.Model;

namespace UnitOfWork.Repositories.Interfaces;

public interface IOrderRepository : IMainRepository<Order>
{
    Dictionary<Customer, decimal> GetClientsMaxSpentMoney();
}