using ADONetShop.DTO;

namespace ADONetShop;

public static class ProductExtensions
{
    public static void PrintProductsToConsole(this ICollection<Product> products)
    {
        foreach (var product in products)
        {
            Console.WriteLine($"Product - {product.ProductName}, Category - {product.CategoryName}");
        }
    }
}