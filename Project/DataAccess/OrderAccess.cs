using Microsoft.Data.Sqlite;

using Dapper;


public class OrderAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Orders";

    public static int Write(OrderModel order)
    {
        string sql = $"INSERT INTO {Table} (Account_ID, Schedule_ID, Amount, Bar) VALUES (@Account_ID, @Schedule_ID, @Amount, @Bar)";
        _connection.Execute(sql, order);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

    public static void Update(OrderModel order)
    {
        string sql = $"UPDATE {Table} SET Amount = @Amount WHERE id = @Id";
        _connection.Execute(sql, order);
    }

    public static List<OrderModel> GetFromAccount(AccountModel account)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_ID = @Id";
        return (List<OrderModel>)_connection.Query<OrderModel>(sql, account);
    }

    public static int GetAvailableBarSpots()
    {
        string sql = @"
SELECT
    Id,
    endTime
FROM Schedule
WHERE endTime < @timeY
AND DATETIME(endTime, '+2 hours')
";

        foreach (int entry in _connection.Query<int>(sql, new { timeX = new DateTime(2028, 12, 12, 19, 0, 0), timeY = new DateTime(2028, 12, 12, 21, 0, 0) }))
            Console.WriteLine(entry);
        return 1;
    }
}