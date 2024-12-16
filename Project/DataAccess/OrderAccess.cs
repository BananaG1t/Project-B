using Microsoft.Data.Sqlite;

using Dapper;


public class OrderAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Orders";

    public static int Write(OrderModel order)
    {
        string sql = $"INSERT INTO {Table} (Account_ID, Schedule_ID, amount, bar) VALUES (@AccountId, @ScheduleId, @Amount, @Bar)";
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

    public static List<(int, DateTime, int)> GetAvailableBarSpots(ScheduleModel schedule)
    {
        string sql = @"
SELECT
    Schedule.Id,
    Schedule.endTime,
    SUM(Orders.amount) AS TotalAmount
FROM Schedule
INNER JOIN Orders ON Schedule.Id = Orders.Schedule_ID
WHERE @StartTime < DATETIME(endTime, '+2 hours') 
AND @EndTime > endTime
AND Location_ID = @LocationId
AND Orders.bar = 1
GROUP BY Schedule.Id, Schedule.endTime;";

        return (List<(int, DateTime, int)>)_connection.Query<(int, DateTime, int)>(sql, new { StartTime = schedule.EndTime, EndTime = schedule.EndTime.AddHours(2), LocationId = schedule.LocationId });
    }

    public static List<OrderModel> GetFromSchedule(ScheduleModel schedule)
    {
        string sql = $"SELECT * FROM {Table} WHERE Schedule_ID = @Id";
        return (List<OrderModel>)_connection.Query<OrderModel>(sql,  schedule);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
}