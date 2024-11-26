using Microsoft.Data.Sqlite;

using Dapper;


public static class ReservationAcces
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "SeatReservations";

    public static void Write(ReservationModel Reservation)
    {
        string sql = $"INSERT INTO {Table} (Account_ID, Schedule_ID, seat_Row, seat_Collum, status) VALUES (@Account_ID, @Schedule_ID, @Seat_Row, @Seat_Collum, @Status)";
        _connection.Execute(sql, Reservation);
    }

    public static ReservationModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ReservationModel>(sql, new { Id = id });
    }

    public static List<ReservationModel> GetFromAccount(AccountModel account)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_ID = @Id";
        return (List<ReservationModel>)_connection.Query<ReservationModel>(sql, account);
    }

    public static List<ReservationModel> GetByAccount_id(int Account_id)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_id = @Account_id";
        return _connection.Query<ReservationModel>(sql, new { Account_id }).ToList();
    }

    public static void Update(ReservationModel reservation)
    {
        string sql = $"UPDATE {Table} SET status = @Status WHERE id = @Id";
        _connection.Execute(sql, reservation);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
}