using Microsoft.Data.Sqlite;

using Dapper;


public static class AuditoriumLayoutAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Auditorium_layout";

    public static double GetPriceBySeatClass(int seatClass)
    {
        string query = "SELECT price FROM Auditorium_layout WHERE seat_class = @seatClass";
        return _connection.QuerySingleOrDefault<double>(query, new { seatClass });
    }

    public static int GetRowSizeByRoomId(int roomId)
    {
        string query = "SELECT MAX(row_num) AS biggest_row_size FROM Auditorium_layout WHERE room_id = @roomId";
        return _connection.QuerySingleOrDefault<int>(query, new { roomId });
    }

    public static int GetColSizeByRoomId(int roomId)
    {
        string query = "SELECT MAX(col_num) AS biggest_row_size FROM Auditorium_layout WHERE room_id = @roomId";
        return _connection.QuerySingleOrDefault<int>(query, new { roomId });
    }

    public static List<AuditoriumLayoutModel> GetById(Int64 id)
    {
        string sql = $"SELECT room_id, row_num, col_num, seat_class, CAST(price AS REAL) AS price FROM {Table} WHERE room_id = @Id";
        List<AuditoriumLayoutModel> Seats = (List<AuditoriumLayoutModel>)_connection.Query<AuditoriumLayoutModel>(sql, new { Id = id });

        return Seats;
    }
}