using System.Data;
using Microsoft.Data.SqlClient;

namespace ADONetShop;

public class ShopDatabaseService
{
    public string ConnectionString { get; set; }

    public ShopDatabaseService(string connectionString) => ConnectionString = connectionString;

    public ICollection<string> GetAllProductsWithCategoriesByDataSet()
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        var sql = "SELECT Product.Name, Category.Category FROM Product " +
                  "Left Join Category On Product.Category_Id = Category.Id;";

        var adapter = new SqlDataAdapter(sql, connection);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);

        var result = new List<string>
            {
                "=========================",
                "Products with categories by DataSet"
            };

        foreach (DataTable table in dataSet.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;

                result.Add($"{cells[0]} {cells[1]}");
            }
        }

        return result;
    }

    public ICollection<string> GetAllProductsWithCategoriesByReader()
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        var sql = "SELECT Product.Name, Category.Category FROM Product " +
                  "Left Join Category On Product.Category_Id = Category.Id;";

        using var command = new SqlCommand(sql, connection);

        using var reader = command.ExecuteReader();

        var result = new List<string>
            {
                "=========================",
                "Products with categories by Reader"
            };

        while (reader.Read())
        {
            result.Add($"{reader[0]} {reader[1]}");
        }

        return result;
    }

    public int GetProductsCount()
    {
        var getProductsCountSql = "Select Count(*) From dbo.Product;";

        var productsCount = ExecuteScalarCommand(getProductsCountSql);

        return productsCount;
    }

    public void AddCategory(string category)
    {
        var createCategorySql = "Insert Into Category(Category) Values(@category);";

        SqlParameter[] sqlParameters =
        {
                new("@category", category) { SqlDbType = SqlDbType.NVarChar }
            };

        ExecuteNonQueryCommand(createCategorySql, sqlParameters);
    }

    public void AddProduct(string productName, int categoryId)
    {
        var insertProductSql = "Insert Into Product(Name, Category_Id) Values(@productName, @categoryId);";

        SqlParameter[] sqlParameters =
        {
                new("@productName", productName) {SqlDbType = SqlDbType.NVarChar},
                new("@categoryId", categoryId) {SqlDbType = SqlDbType.Int}
            };

        ExecuteNonQueryCommand(insertProductSql, sqlParameters);
    }

    public void UpdateProduct(int productId, string newProductName)
    {
        var updateProductSql = "Update Product Set Name=@newProductName Where Id=@productId;";

        SqlParameter[] sqlParameters =
        {
                new("@productId", productId) {SqlDbType = SqlDbType.Int},
                new("@newProductName", newProductName) {SqlDbType = SqlDbType.NVarChar}
            };

        ExecuteNonQueryCommand(updateProductSql, sqlParameters);
    }

    public void DeleteProductById(int productId)
    {
        var createCategorySql = "DELETE FROM Product WHERE Id=@productId";

        SqlParameter[] sqlParameters = { new("@productId", productId) { SqlDbType = SqlDbType.Int } };

        ExecuteNonQueryCommand(createCategorySql, sqlParameters);
    }

    private void ExecuteNonQueryCommand(string sqlQuery, params SqlParameter[] sqlParameters)
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        using var command = new SqlCommand(sqlQuery, connection);

        command.Parameters.AddRange(sqlParameters);

        command.ExecuteNonQuery();
    }

    private int ExecuteScalarCommand(string sqlQuery)
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        using var command = new SqlCommand(sqlQuery, connection);

        return (int)command.ExecuteScalar();
    }
}