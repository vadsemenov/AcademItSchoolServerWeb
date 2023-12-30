namespace ADONetTransaction
{
    public class Program
    {
        private const string ConnectionString = "Data Source=.;" +
                                                "Initial Catalog=Shop;" +
                                                "Integrated Security=true;" +
                                                "TrustServerCertificate=true";

        public static void Main(string[] args)
        {
            var dataService = new ShopDatabaseService(ConnectionString);

            dataService.AddCategoryWithoutTransaction("Category1");
            dataService.AddCategoryWithTransaction("Category2");

            var categories = dataService.GetAllCategoriesByDataSet();

            categories.PrintCollectionToConsole();

            Console.Read();
        }
    }
}
