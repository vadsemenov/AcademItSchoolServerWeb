using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ADONetShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dataService = new ShopDatabaseService("Data Source=.;" +
                                              "Initial Catalog=Shop;" +
                                              "Integrated Security=true;" +
                                              "TrustServerCertificate=true");

            Console.WriteLine(dataService.GetProductsCount());

            // dataService.AddCategory("Carpet");
            // dataService.AddProduct("Cherry", 1);

            // dataService.DeleteProduct(4);



            Console.Read();
        }

        public class ShopDatabaseService
        {
            public string ConnectionString { get; set; }

            public ShopDatabaseService(string connectionString)
            {
                ConnectionString = connectionString;
            }

            //Todo finish
            public void PrintAllProductsWithCategories()
            {
                using var connection = new SqlConnection(ConnectionString);
                
                    connection.Open();

                    var sql = "SELECT Product.Name, Category.Category FROM Product"+
                    "Left Join Category On Product.Category_Id = Category.Id;";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Тут обращаемся к столбцам по индексам
                                // Но лучше через имя столбца
                                Console.WriteLine(“{ 0}
                                { 1}”, reader[0], reader[1]);
                            }
                        }
                    }
            }

            public int GetProductsCount()
            {
                using var connection = new SqlConnection(ConnectionString);

                var getProductsCountSql = "Select Count(*) From dbo.Product;";

                connection.Open();

                using var command = new SqlCommand(getProductsCountSql, connection);

                var productsCount = (int)command.ExecuteScalar();

                return productsCount;
            }

            public void AddCategory(string category)
            {
                using var connection = new SqlConnection(ConnectionString);

                var createCategorySql = "Insert Into Category(Category) Values(@category);";

                connection.Open();

                using var command = new SqlCommand(createCategorySql, connection);

                command.Parameters.Add(new SqlParameter("@category", category) { SqlDbType = SqlDbType.NVarChar });

                command.ExecuteNonQuery();
            }

            public void AddProduct(string productName, int categoryId)
            {
                using var connection = new SqlConnection(ConnectionString);

                var createCategorySql = "Insert Into Product(Name, Category_Id) Values(@productName, @categoryId);";

                connection.Open();

                using var command = new SqlCommand(createCategorySql, connection);

                command.Parameters.Add(new SqlParameter("@productName", productName) { SqlDbType = SqlDbType.NVarChar });
                command.Parameters.Add(new SqlParameter("@categoryId", categoryId) { SqlDbType = SqlDbType.Int });

                command.ExecuteNonQuery();
            }

            public void UpdateProduct(int productId, string newProductName)
            {
                using var connection = new SqlConnection(ConnectionString);

                var createCategorySql = "Update Product Set Name=@newProductName Where Id=@productId;";

                connection.Open();

                using var command = new SqlCommand(createCategorySql, connection);

                command.Parameters.Add(new SqlParameter("@productId", productId) { SqlDbType = SqlDbType.Int });
                command.Parameters.Add(new SqlParameter("@newProductName", newProductName) { SqlDbType = SqlDbType.NVarChar });

                command.ExecuteNonQuery();
            }

            public void DeleteProduct(int productId)
            {
                using var connection = new SqlConnection(ConnectionString);

                var createCategorySql = "DELETE FROM Product WHERE Id=@productId";

                connection.Open();

                using var command = new SqlCommand(createCategorySql, connection);

                command.Parameters.Add(new SqlParameter("@productId", productId) { SqlDbType = SqlDbType.Int });

                command.ExecuteNonQuery();
            }
        }
    }
}
