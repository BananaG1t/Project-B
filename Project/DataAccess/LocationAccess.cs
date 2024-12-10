using Microsoft.Data.Sqlite;

using Dapper;

public static class LocationAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "Location";

    public static Int64 Write(LocationModel location)
    {
        string sql = $"INSERT INTO {Table} (name) VALUES (@Name)";
        _connection.Execute(sql, location);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }
    public static void Update(LocationModel snack)
    {
        string sql = $"UPDATE {Table} SET name = @Name WHERE id = @Id";
        _connection.Execute(sql, snack);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
    public static LocationModel GetById(int id)
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
        string sql = @$"SELECT DISTINCT Location.* FROM {Table} LEFT JOIN Schedule ON {Table}.id = Schedule.Location_ID WHERE Schedule.Location_ID IS NULL";
        List<LocationModel> locations = (List<LocationModel>)_connection.Query<LocationModel>(sql);

        return locations;
    }

    public static List<string> GetAllNames()
    {
        string sql = $"SELECT name FROM {Table}";
        List<string> LocationsNames = (List<string>)_connection.Query<string>(sql);

        return LocationsNames;
    }

}