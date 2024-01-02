using System.Data;
using Microsoft.Data.SqlClient;

namespace ADONetTransaction;

public class ShopDatabaseService
{
    public string ConnectionString { get; set; }

    public ShopDatabaseService(string connectionString) => ConnectionString = connectionString;

    public ICollection<string> GetAllCategoriesByDataSet()
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        var sql = "SELECT Category.Category FROM Category ";

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

                result.Add($"{cells[0]}");
            }
        }

        return result;
    }

    public void AddCategory(string category, bool useTransaction = false)
    {
        var createCategorySql = "Insert Into Category(Category) Values(@category);";

        SqlParameter[] sqlParameters =
        {
            new("@category", category) { SqlDbType = SqlDbType.NVarChar }
        };

        if (useTransaction)
        {
            ExecuteNonQueryCommandWithTransaction(createCategorySql, sqlParameters);
        }
        else
        {
            ExecuteNonQueryCommandWithoutTransaction(createCategorySql, sqlParameters);
        }
    }

    private void ExecuteNonQueryCommandWithoutTransaction(string sqlQuery, params SqlParameter[] sqlParameters)
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        using var command = new SqlCommand(sqlQuery, connection);

        command.Parameters.AddRange(sqlParameters);

        command.ExecuteNonQuery();

        throw new Exception("Exception without transaction.");
    }

    private void ExecuteNonQueryCommandWithTransaction(string sqlQuery, params SqlParameter[] sqlParameters)
    {
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        var transaction = connection.BeginTransaction();

        try
        {
            using var command = new SqlCommand(sqlQuery, connection);

            command.Parameters.AddRange(sqlParameters);

            command.ExecuteNonQuery();

            throw new Exception("Exception without transaction.");
        }
        catch (Exception exception)
        {
            transaction.Rollback();
        }
    }
}