using EFMigrate.Model;

namespace EFMigrate;

public static class ShopService
{
    public static List<Product> GetMostFrequentlyPurchasedProducts(ShopDbContext shopContext)
    {
        var orderItems = shopContext.OrderItems;

        var products = orderItems
            .GroupBy(o => o.Product)
            .Select(g => new
            {
                Product = g.Key,
                ProductCount = g.Sum(orderItem => orderItem.Count)
            })
            .Where(pg => pg.ProductCount == orderItems
                .GroupBy(oi => oi.Product)
                .Select(oig => oig.Sum(c => c.Count))
                .Max())
            .Select(pg => pg.Product)
            .ToList();

        return products;
    }

    public static Dictionary<Customer, decimal> GetClientsMaxSpentMoney(ShopDbContext shopContext)
    {
        var maxSpentMoney = shopContext.Orders
            .GroupBy(o => o.Customer)
            .Select(g => new
            {
                Customer = g.Key,
                SpenMoney = g.SelectMany(o => o.OrderItems).Select(oi => oi.Product.Price).Sum()
            })
            .ToDictionary(k => k.Customer, v => v.SpenMoney);

        return maxSpentMoney;
    }

    public static Dictionary<string, int> GetCategoriesProductsCount(ShopDbContext shopContext)
    {
        var categoriesProductsCount = shopContext.Categories
            .Select(c => new
            {
                c.Name,
                ProductsCount = c.Products.SelectMany(p => p.OrderItems).Select(oi => oi.Count).Sum()
            })
            .GroupBy(k=> k.Name)
            .ToDictionary(k => k.Key, v => v.Sum(g=>g.ProductsCount));

        return categoriesProductsCount;
    }
}