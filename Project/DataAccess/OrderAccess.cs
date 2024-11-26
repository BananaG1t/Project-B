using Microsoft.Data.Sqlite;

using Dapper;


public class OrderAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Oders";

    public static int Write(OrderModel order)
    {
        string sql = $"INSERT INTO {Table} (Account_ID, Schedule_ID, Amount, Bar) VALUES (@Account_ID, @Schedule_ID, @Amount, @Bar)";
        _connection.Execute(sql, order);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }
}