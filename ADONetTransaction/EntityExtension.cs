using ADONetTransaction.DTO;

namespace ADONetTransaction;

public static class EntityExtensions
{
    public static void PrintCategoriesToConsole(this ICollection<Category> categories)
    {
        foreach (var category in categories)
        {
            Console.WriteLine($"Category - {category.CategoryName}");
        }
    }
}