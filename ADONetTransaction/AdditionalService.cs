namespace ADONetTransaction;

public static class AdditionalService
{
    public static void PrintCollectionToConsole(this ICollection<string> collection)
    {
        foreach (var value in collection)
        {
            Console.WriteLine(value);
        }
    }
}