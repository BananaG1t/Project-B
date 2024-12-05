using Microsoft.Data.Sqlite;

using Dapper;

public static class RoleAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Roles";

    public static Int64 Write(RoleModel RoleLevel)
    {
        string sql = $"INSERT INTO {Table} (name, level_Access) VALUES (@RoleName, @LevelAccess)";
        _connection.Execute(sql, RoleLevel);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }

    public static void Update(RoleModel RoleLevel)
    {
        string sql = $"UPDATE {Table} SET name = @RoleName, level_Access = @LevelAccess WHERE id = @Id";
        _connection.Execute(sql, RoleLevel);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static RoleModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { Id = id });
    }

    public static RoleModel GetByRoleLevelAccess(int LevelAccessMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE level_Access = @LevelAccess";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { LevelAccess = LevelAccessMethod });
    }

    public static RoleModel GetByName(string RoleNameMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE name = @RoleName";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { RoleName = RoleNameMethod });
    }

    public static List<RoleModel> GetAllRoles()
    {
        string sql = $"SELECT * FROM {Table} ORDER BY level_Access ASC";
        return (List<RoleModel>)_connection.Query<RoleModel>(sql, new { });
    }

}