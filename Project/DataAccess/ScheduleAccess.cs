using Microsoft.Data.Sqlite;

using Dapper;


public static class ScheduleAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Schedule";

    public static int Write(ScheduleModel schedule)
    {
        string sql = $"INSERT INTO {Table} (startTime, endTime, Movie_ID, Auditorium_ID, Location_ID) VALUES (@StartTime, @EndTime, @MovieId, @AuditoriumId, @LocationId)";
        _connection.Execute(sql, schedule);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

    public static bool IsAvailable(int room, DateTime startTime, DateTime endTime, int location)
    {
        string sql = @$"
                SELECT COUNT(*) FROM {Table} 
                JOIN Auditorium ON {Table}.Auditorium_ID = Auditorium.id 
                WHERE Auditorium.room = @Room 
                AND @StartTime < endTime 
                AND @EndTime > startTime
                AND Location_ID = @Location";
        return _connection.ExecuteScalar<int>(sql, new { Room = room, StartTime = startTime, EndTime = endTime, Location = location }) == 0;
    }

    public static List<ScheduleModel> ScheduleByDate()
    {
        DateTime currdate = DateTime.Now;
        string sql = $"SELECT * FROM {Table} WHERE startTime > @Currdate ORDER BY startTime ASC";
        List<ScheduleModel> schedules = (List<ScheduleModel>)_connection.Query<ScheduleModel>(sql, new { Currdate = currdate });

        return schedules;
    }

    public static List<ScheduleModel> ScheduleByDateAndLocation(LocationModel location)
    {
        DateTime currdate = DateTime.Now;
        string sql = $"SELECT * FROM {Table} WHERE startTime > @Currdate AND Location_ID = @LocationId ORDER BY startTime ASC";
        List<ScheduleModel> schedules = (List<ScheduleModel>)_connection.Query<ScheduleModel>(sql, new { Currdate = currdate, LocationId = location.Id });

        return schedules;
    }

    public static List<LocationModel> GetAllLocationsWithSchedules()
    {
        DateTime currdate = DateTime.Now;
        string sql = @$"SELECT DISTINCT Location.* FROM {Table} JOIN Location ON {Table}.Location_ID = Location.id WHERE startTime > @Currdate";
        List<LocationModel> locations = (List<LocationModel>)_connection.Query<LocationModel>(sql, new { Currdate = currdate });

        return locations;
    }
    public static List<ScheduleModel> GetByDateRange(DateTime startTime, DateTime endTime)
    {
        string sql = $"SELECT * FROM {Table} WHERE StartTime < endTime AND @EndTime > startTime";
        return _connection.Query<ScheduleModel>(sql, new { StartTime = startTime, EndTime = endTime }).ToList();
    }

    public static List<ScheduleModel> GetByMovieId(int Movie_ID)
    {
        string sql = $"SELECT * FROM {Table} WHERE Movie_ID = @MovieId";
        return _connection.Query<ScheduleModel>(sql, new { MovieId = Movie_ID }).ToList();
    }

    public static ScheduleModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ScheduleModel>(sql, new { Id = id });
    }

    public static List<ScheduleModel> GetByLocationId(int locId)
    {
        string sql = $"SELECT * FROM {Table} WHERE Location_ID = @Id";
        return (List<ScheduleModel>)_connection.Query<ScheduleModel>(sql, new { Id = locId });
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

    public static List<ScheduleModel> GetpastSchedules()
    {
        DateTime currentTime = DateTime.Now;
        string sql = $"SELECT * FROM {Table} WHERE startTime < @CurrentTime";
        return (List<ScheduleModel>)_connection.Query<ScheduleModel>(sql, new { CurrentTime = currentTime });
    }

    public static List<ScheduleModel> GetpastSchedulesWithMovie(MovieModel movie)
    {
        DateTime currentTime = DateTime.Now;
        string sql = $"SELECT * FROM {Table} WHERE startTime < @CurrentTime AND Movie_ID = @MovieId";
        return (List<ScheduleModel>)_connection.Query<ScheduleModel>(sql, new { CurrentTime = currentTime, MovieId = movie.Id });
    }

    public static List<SeatModel> GetSeats(ScheduleModel schedule)
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