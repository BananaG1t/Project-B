using Microsoft.Data.Sqlite;

using Dapper;


public class BarReservationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Bar_reservation";

    public static Int64 Write(BarSeatModel BarSeat)
    {
        string sql = $"INSERT INTO {Table} (startTime, endTime, Account_ID, Reservation_ID, Seat_Number) VALUES (@startTime, @endTime, @Account_ID, @Reservation_ID, @Seat_Number)";
        _connection.Execute(sql, BarSeat);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }

    public static List<int> IsAvailable(DateTime startTime, DateTime endTime)
    {
        string sql = @$"WITH RECURSIVE PossibleSpots AS (
                        SELECT 1 AS Seat_Number
                        UNION ALL
                        SELECT Seat_Number + 1
                        FROM PossibleSpots
                        WHERE Seat_Number < 40
                    )
                    SELECT Seat_Number
                    FROM PossibleSpots
                    WHERE Seat_Number NOT IN (
                        SELECT Seat_Number
                        FROM {Table}
                        WHERE (startTime < @StartTime AND endTime > @EndTime)
                    )
                    ORDER BY Seat_Number";

        return (List<int>)_connection.Query<int>(sql, new { StartTime = startTime, EndTime = endTime });
    }



}