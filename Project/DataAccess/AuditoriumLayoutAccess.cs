using Microsoft.Data.Sqlite;

using Dapper;


public static class AuditoriumLayoutAcces
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Auditorium_layout";


    public static List<AuditoriumLayoutModel> GetById(Int64 id)
    {
        string sql = $"SELECT room_id, row_num, col_num, seat_class, CAST(price AS REAL) AS price FROM {Table} WHERE room_id = @Id";
        List<AuditoriumLayoutModel> Seats = (List<AuditoriumLayoutModel>)_connection.Query<AuditoriumLayoutModel>(sql, new { Id = id });

        return Seats;
    }



}