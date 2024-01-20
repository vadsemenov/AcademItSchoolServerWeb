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

            dataService.AddCategory("Category1");
            dataService.AddCategory("Category1", true);

            var categories = dataService.GetAllCategoriesByDataSet();

            Console.WriteLine("=========================");
            Console.WriteLine("Categories by DataSet");
            categories.PrintCategoriesToConsole();

            Console.Read();
        }
    }
}
