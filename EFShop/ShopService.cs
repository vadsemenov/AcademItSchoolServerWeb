using EFShop.Model;

namespace EFShop;

public static class ShopService
{
    public static List<Product> GetMostFrequentlyPurchasedProducts(ShopDbContext shopContext)
    {
        var orderItems = shopContext.OrderItems;

        return orderItems
            .GroupBy(o => o.Product)
            .Select(g => new
            {
                Product = g.Key,
                ProductsCount = g.Sum(orderItem => orderItem.Count)
            })
            .Where(pg => pg.ProductsCount == orderItems
                .GroupBy(oi => oi.Product)
                .Select(oig => oig.Sum(c => c.Count))
                .Max())
            .Select(pg => pg.Product)
            .ToList();
    }

    public static Dictionary<Customer, decimal> GetClientsMaxSpentMoney(ShopDbContext shopContext)
    {
        return shopContext.Orders
            .GroupBy(o => o.Customer)
            .Select(g => new
            {
                Customer = g.Key,
                SpenMoney = g.SelectMany(o => o.OrderItems).Sum(oi => oi.Product.Price)
            })
            .ToDictionary(k => k.Customer, v => v.SpenMoney);
    }

    public static Dictionary<string, int> GetCategoriesProductsCount(ShopDbContext shopContext)
    {
        return shopContext.Categories
            .Select(c => new
            {
                c.Name,
                ProductsCount = c.Products.SelectMany(p => p.OrderItems).Sum(oi => oi.Count)
            })
            .ToDictionary(k => k.Name, v => v.ProductsCount);
    }
}