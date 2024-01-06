using UnitOfWork.Model;

namespace UnitOfWork.Repositories.Interfaces;

public interface ICategoryRepository : IMainRepository<Category>
{
    Dictionary<string, int> GetCategoriesProductsCount();
}