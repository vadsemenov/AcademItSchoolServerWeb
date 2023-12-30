namespace ADONetShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataService = new ShopDatabaseService("Data Source=.;" +
                                              "Initial Catalog=Shop;" +
                                              "Integrated Security=true;" +
                                              "TrustServerCertificate=true");

            Console.WriteLine("Products count: " + dataService.GetProductsCount());

            dataService.AddCategory("Carpet");
            dataService.AddProduct("Cherry", 1);

            dataService.DeleteProductById(4);

            dataService.UpdateProduct(1, "doll");

            var productsByReader = dataService.GetAllProductsWithCategoriesByReader();
            productsByReader.PrintCollectionToConsole();

            var productsByDataSet = dataService.GetAllProductsWithCategoriesByDataSet();
            productsByDataSet.PrintCollectionToConsole();

            Console.Read();
        }
    }
}
