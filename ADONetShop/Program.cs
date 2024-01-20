namespace ADONetShop
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

            Console.WriteLine("Products count: " + dataService.GetProductsCount());
            
            dataService.AddCategory("Carpet");
            dataService.AddProduct("Cherry", 1);
            
            dataService.DeleteProductById(4);
            
            dataService.UpdateProduct(1, "doll");
            
            var productsByReader = dataService.GetAllProductsWithCategoriesByReader();
            Console.WriteLine("=========================");
            Console.WriteLine("Products with categories by Reader");
            productsByReader.PrintProductsToConsole();

            var productsByDataSet = dataService.GetAllProductsWithCategoriesByDataSet();
            Console.WriteLine("=========================");
            Console.WriteLine("Products with categories by DataSet");
            productsByDataSet.PrintProductsToConsole();

            Console.Read();
        }
    }
}