using Microsoft.Data.Sqlite;

using Dapper;

public static class RoleLevelAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "RoleLevel";

    public static int Write(RoleLevelModel RoleLevel)
    {
        string sql = $"INSERT INTO {Table} (functionality, level_Needed) VALUES (@Functionalty, @LevelNeeded)";
        _connection.Execute(sql, RoleLevel);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }
    public static void Update(RoleLevelModel RoleLevel)
    {
        string sql = $"UPDATE {Table} SET functionality = @Functionality, level_Needed = @LevelNeeded WHERE id = @Id";
        _connection.Execute(sql, RoleLevel);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static RoleLevelModel? GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<RoleLevelModel>(sql, new { Id = id });
    }

    public static RoleLevelModel? GetByLevel(int level)
    {
        string sql = $"SELECT * FROM {Table} WHERE level_Needed = @LevelNeeded";
        return _connection.QueryFirstOrDefault<RoleLevelModel>(sql, new { LevelNeeded = level });
    }

    public static RoleLevelModel? GetByFunctionality(string FunctionalityName)
    {
        string sql = $"SELECT * FROM {Table} WHERE functionality = @Functionality";
        return _connection.QueryFirstOrDefault<RoleLevelModel>(sql, new { Functionality = FunctionalityName });
    }

    public static List<RoleLevelModel> GetAllRoleLevels()
    {
        string sql = $"SELECT * FROM {Table} ORDER BY level_Needed DESC";
        return (List<RoleLevelModel>)_connection.Query<RoleLevelModel>(sql, new { });
    }

}