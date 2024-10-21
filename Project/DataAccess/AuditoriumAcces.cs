using Microsoft.Data.Sqlite;

using Dapper;


public static class AuditoriumAcces
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Auditorium";

    public static Int64 Write(AuditoriumModel auditorium)
    {
        string sql = $"INSERT INTO {Table} (startTime, endTime, Movie_ID, Auditorium_ID) VALUES (@StartTime, @EndTime, @Movie.Id, @Auditorium.Id)";
        _connection.Execute(sql, auditorium);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }


    public static AuditoriumModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<AuditoriumModel>(sql, new { Id = id });
    }

    public static AuditoriumModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<AuditoriumModel>(sql, new { Email = email });
    }

    public static void Update(AuditoriumModel auditorium)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        _connection.Execute(sql, auditorium);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }



}