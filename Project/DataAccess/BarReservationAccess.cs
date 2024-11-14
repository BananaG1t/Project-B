using Microsoft.Data.Sqlite;

using Dapper;


public class BarReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Barseats";

    public static Int64 Write(BarSeatModel BarSeat)
    {
        string sql = $"INSERT INTO {Table} (Auditorium_ID, row, collum, price, type) VALUES (@AuditoriumId, @Row, @Collum, @Price, @Class)";
        _connection.Execute(sql, BarSeat);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }

    public static bool IsAvailable(int Seat, DateTime startTime, DateTime endTime)
    {
        string sql = @$"
                SELECT COUNT(*) FROM {Table} 
                JOIN Auditorium ON {Table}.Auditorium_ID = Auditorium.id 
                WHERE Auditorium.room = @Room 
                AND @StartTime < endTime 
                AND @EndTime > startTime";
        return _connection.ExecuteScalar<int>(sql, new { Room = Seat, StartTime = startTime, EndTime = endTime }) == 0;
    }

}