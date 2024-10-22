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
}