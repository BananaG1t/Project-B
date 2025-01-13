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

    public static List<SeatModel> GetByRoom(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE Auditorium_ID = @Id";
        return (List<SeatModel>)_connection.Query<SeatModel>(sql, new { Id = id });
    }

    public static AccountModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Id = id });
    }

    public static List<SeatModel> GetFromAuditorium(AuditoriumModel auditorium)
    {
        string sql = $"SELECT * FROM {Table} WHERE Auditorium_ID = @Id";
        return (List<SeatModel>)_connection.Query<SeatModel>(sql, auditorium);
    }

    public static List<SeatModel> GetAll()
    {
        string sql = $"SELECT * FROM {Table}";
        List<SeatModel> seats = (List<SeatModel>)_connection.Query<SeatModel>(sql);

        return seats;
    }

    public static AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public static void Update(SeatModel seat)
    {
        string sql = $"UPDATE {Table} SET IsAvailable = @IsAvailable WHERE id = @Id";
        _connection.Execute(sql, seat);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static SeatModel GetByReservationInfo(int ColNum, int RowNum, int Auditorium_ID)
    {
        string sql = $"SELECT * FROM {Table} WHERE collum = @ColNum AND row = @RowNum AND Auditorium_ID = @AuditoriumId";
        return _connection.QueryFirstOrDefault<SeatModel>(sql, new { ColNum = ColNum, RowNum = RowNum, AuditoriumId = Auditorium_ID });
    }
}