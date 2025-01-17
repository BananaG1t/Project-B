using Microsoft.Data.Sqlite;

using Dapper;

public static class LocationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static readonly string Table = "Location";

    public static int Write(LocationModel location)
    {
        string sql = $"INSERT INTO {Table} (name) VALUES (@Name)";
        _connection.Execute(sql, location);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }
    public static void Update(LocationModel snack)
    {
        string sql = $"UPDATE {Table} SET name = @Name WHERE id = @Id";
        _connection.Execute(sql, snack);
    }

    public static LocationModel? GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<LocationModel>(sql, new { Id = id });
    }

    public static List<LocationModel> GetAll()
    {
        string sql = $"SELECT id, name FROM {Table}";
        List<LocationModel> Locations = (List<LocationModel>)_connection.Query<LocationModel>(sql);

        return Locations;
    }

    public static List<LocationModel> GetAllLocationsWithNoSchedules()
    {
        DateTime currdate = DateTime.Now;
        string sql = @$"
                        SELECT l.*
                        FROM {Table} l
                        LEFT JOIN Schedule s ON l.id = s.Location_ID
                        GROUP BY l.id
                        HAVING COUNT(s.id) = 0 OR MAX(s.StartTime) < @Currdate";        List<LocationModel> locations = (List<LocationModel>)_connection.Query<LocationModel>(sql, new { Currdate = currdate });

        return locations;
    }

    public static List<string> GetAllNames()
    {
        string sql = $"SELECT name FROM {Table}";
        List<string> LocationsNames = (List<string>)_connection.Query<string>(sql);

        return LocationsNames;
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
    _connection.Execute(sql, new { Id = id });
    }
}