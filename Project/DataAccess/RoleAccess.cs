using Microsoft.Data.Sqlite;

using Dapper;

public static class RoleAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static readonly string Table = "Roles";

    public static int Write(RoleModel Role)
    {
        string sql = $"INSERT INTO {Table} (name, level_Access) VALUES (@Name, @LevelAccess)";
        _connection.Execute(sql, Role);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

    public static void Update(RoleModel Role)
    {
        string sql = $"UPDATE {Table} SET name = @RoleName, level_Access = @LevelAccess WHERE id = @Id";
        _connection.Execute(sql, Role);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static RoleModel? GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { Id = id });
    }

    public static RoleModel? GetByRoleLevelAccess(int LevelAccessMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE level_Access = @LevelAccess";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { LevelAccess = LevelAccessMethod });
    }

    public static RoleModel? GetByName(string RoleNameMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE name = @RoleName";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { RoleName = RoleNameMethod });
    }

    public static List<RoleModel> GetAllRoles()
    {
        string sql = $"SELECT * FROM {Table} ORDER BY level_Access DESC";
        return (List<RoleModel>)_connection.Query<RoleModel>(sql, new { });
    }

}