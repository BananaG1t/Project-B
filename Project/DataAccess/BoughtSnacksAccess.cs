using Microsoft.Data.Sqlite;

using Dapper;


public static class BoughtSnacksAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "BoughtSnacks";

    public static Int64 Write(BoughtSnacksModel snack)
    {
        string sql = $"INSERT INTO {Table} (Reservation_ID, Snack_ID, amount) VALUES (@ReservationId, @SnackId, @Amount)";
        _connection.Execute(sql, snack);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

    public static void Write(int reservation_id, int snack_id, int amount)
    {
        string sql = $"INSERT INTO {Table} (Reservation_ID, Snack_ID, amount) VALUES (@ReservationId, @SnackId, @Amount)";
        _connection.Execute(sql, new { ReservationId = reservation_id, SnackId = snack_id, Amount = amount });
    }

    public static BoughtSnacksModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<BoughtSnacksModel>(sql, new { Id = id });
    }

    public static List<BoughtSnacksModel> GetFromReservation(ReservationModel reservations)
    {
        string sql = $"SELECT * FROM {Table} WHERE Reservation_ID = @Id";
        return (List<BoughtSnacksModel>)_connection.Query<BoughtSnacksModel>(sql, reservations);
    }

    public static List<BoughtSnacksModel> GetAll()
    {
        string sql = $"SELECT Account_ID,Reservation_ID,snack,amount FROM {Table}";
        List<BoughtSnacksModel> Snacks = (List<BoughtSnacksModel>)_connection.Query<BoughtSnacksModel>(sql);

        return Snacks;
    }

    public static void Update(SnacksModel snack)
    {
        string sql = $"UPDATE {Table} SET name = @Name, price = @Price WHERE id = @Id";
        _connection.Execute(sql, snack);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static BoughtSnacksModel? GetByReservationId(int Reservation_ID)
    {
        string sql = $"SELECT * FROM {Table} WHERE Reservation_ID = @ReservationId";
        return _connection.QueryFirstOrDefault<BoughtSnacksModel>(sql, new { ReservationId = Reservation_ID });
    }
}