using Microsoft.Data.Sqlite;

using Dapper;


public static class SeatsAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Seats";

    public static Int64 Write(SeatModel seat)
    {
        string sql = $"INSERT INTO {Table} (Auditorium_ID, row, collum, price, type) VALUES (@AuditoriumId, @Row, @Collum, @Price, @Class)";
        _connection.Execute(sql, seat);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }


    public static AccountModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Id = id });
    }

    public static AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public static void Update(AccountModel account)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        _connection.Execute(sql, account);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }



}