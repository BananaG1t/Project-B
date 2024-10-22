using Microsoft.Data.Sqlite;

using Dapper;


public static class ScheduleAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Schedule";

    public static Int64 Write(ScheduleModel schedule)
    {
        string sql = $"INSERT INTO {Table} (startTime, endTime, Movie_ID, Auditorium_ID) VALUES (@StartTime, @EndTime, @MovieId, @AuditoriumId)";
        _connection.Execute(sql, schedule);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }

    public static bool IsAvailable(int room, DateTime startTime, DateTime endTime)
    {
        string sql = @$"
                SELECT COUNT(*) FROM {Table} 
                JOIN Auditorium ON {Table}.Auditorium_ID = Auditorium.id 
                WHERE Auditorium.room = @Room 
                AND @StartTime < endTime 
                AND @EndTime > startTime";
        return _connection.ExecuteScalar<int>(sql, new { Room = room, StartTime = startTime, EndTime = endTime }) == 0;
    }

    public static ScheduleModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ScheduleModel>(sql, new { Id = id });
    }

    public static ScheduleModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<ScheduleModel>(sql, new { Email = email });
    }

    public static void Update(ScheduleModel schedule)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        _connection.Execute(sql, schedule);
    }

    public static List <ScheduleModel> GetpastSchedules()
    {
        DateTime currentTime = DateTime.Now;
        string sql = $"SELECT * FROM {Table} WHERE startTime < @CurrentTime";
        return (List<ScheduleModel>)_connection.Query<ScheduleModel>(sql, new { CurrentTime = currentTime });
    }
        public static List <SeatModel> GetSeats(ScheduleModel schedule)
    {
        string sql = @$"SELECT * FROM Seats 
        WHERE Auditorium_ID = @AuditoriumId";
        return (List<SeatModel>)_connection.Query<SeatModel>(sql, schedule);
    }
    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }



}