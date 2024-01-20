using System.Collections;
using System.Data;
using ADONetShop.DTO;
using Microsoft.Data.SqlClient;

namespace ADONetShop;

public class ShopDatabaseService
{
    public string ConnectionString { get; set; }

    public ShopDatabaseService(string connectionString) => ConnectionString = connectionString;

    public ICollection<Product> GetAllProductsWithCategoriesByDataSet()
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        var sql = """SELECT Product.Name, Category.Name """ +
                  """FROM Product """ +
                  """LEFT JOIN Category """ +
                  """  ON Product.CategoryId = Category.Id;""";

        var adapter = new SqlDataAdapter(sql, connection);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);

        var result = new List<Product>();

        foreach (DataTable table in dataSet.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                var cells = row.ItemArray;

                result.Add(new Product(cells[0] as string, cells[1] as string));
            }
        }

        return result;
    }

    public ICollection<Product> GetAllProductsWithCategoriesByReader()
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        var sql = """SELECT Product.Name, Category.Name """ +
                  """FROM Product """ +
                  """LEFT JOIN Category """ +
                  """ ON Product.CategoryId = Category.Id;""";

        using var command = new SqlCommand(sql, connection);

        using var reader = command.ExecuteReader();

        var result = new List<Product>();

        while (reader.Read())
        {
            result.Add(new Product(reader[0] as string, reader[1] as string));
        }

        return result;
    }

    public int GetProductsCount()
    {
        var getProductsCountSql = """SELECT Count(*) """ +
                                  """FROM dbo.Product;""";

        var productsCount = ExecuteScalarCommand(getProductsCountSql);

        return productsCount;
    }

    public void AddCategory(string category)
    {
        var createCategorySql = """INSERT INTO Category(Name) """ +
                                """VALUES (@category);""";

        SqlParameter[] sqlParameters =
        {
            new("@category", category) {SqlDbType = SqlDbType.NVarChar}
        };

        ExecuteNonQueryCommand(createCategorySql, sqlParameters);
    }

    public void AddProduct(string productName, int categoryId)
    {
        var insertProductSql = """INSERT INTO Product(Name, CategoryId) """ +
                               """VALUES (@productName, @categoryId);""";

        SqlParameter[] sqlParameters =
        {
            new("@productName", productName) {SqlDbType = SqlDbType.NVarChar},
            new("@categoryId", categoryId) {SqlDbType = SqlDbType.Int}
        };

        ExecuteNonQueryCommand(insertProductSql, sqlParameters);
    }

    public void UpdateProduct(int productId, string newProductName)
    {
        var updateProductSql = """UPDATE Product """ +
                               """SET Name = @newProductName """ +
                               """WHERE Id = @productId;""";

        SqlParameter[] sqlParameters =
        {
            new("@productId", productId) {SqlDbType = SqlDbType.Int},
            new("@newProductName", newProductName) {SqlDbType = SqlDbType.NVarChar}
        };

        ExecuteNonQueryCommand(updateProductSql, sqlParameters);
    }

    public void DeleteProductById(int productId)
    {
        var createCategorySql = """DELETE FROM Product """ +
                                """WHERE Id = @productId""";

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