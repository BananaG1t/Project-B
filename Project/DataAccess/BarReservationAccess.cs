using Microsoft.Data.Sqlite;

using Dapper;


public class BarReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Bar_reservation";

    public static Int64 Write(BarSeatModel BarSeat)
    {
        string sql = $"INSERT INTO {Table} (startTime, endTime, Account_ID, Reservation_ID, Seat_Number) VALUES (@StartTime, @EndTime, @AccountId, @ReservationId, @SeatId)";
        _connection.Execute(sql, BarSeat);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }

    public static List<int> IsAvailable(DateTime startTime, DateTime endTime)
    {
        List<int> ValidSeats = [];

        string sql = @$"
                SELECT COUNT(*) FROM {Table} 
                WHERE Seat_Number = @SeatNumber 
                AND @StartTime < endTime 
                AND @EndTime > startTime";

        for (int CurrentSeatNumber = 1; CurrentSeatNumber < 41; CurrentSeatNumber++)
        {
            if (_connection.ExecuteScalar<int>(sql, new { StartTime = startTime, EndTime = endTime, SeatNumber = CurrentSeatNumber }) == 0)
                ValidSeats.Add(CurrentSeatNumber);
        }

        return ValidSeats;
    }

    public static void WipeTable()
    {
        string sql = $"DELETE FROM {Table}";
        _connection.Execute(sql);
    }



}