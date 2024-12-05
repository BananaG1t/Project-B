using Microsoft.Data.Sqlite;

using Dapper;

public static class RoleAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Roles";

    public static Int64 Write(RoleModel RoleLevel)
    {
        string sql = $"INSERT INTO {Table} (Role_Name, Role_Level_ID) VALUES (@RoleName, @RoleLevelId)";
        _connection.Execute(sql, RoleLevel);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }

    public static RoleModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { Id = id });
    }

    public static RoleModel GetByRoleLevelId(int RolelevelIdMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE Role_Level_ID = @RolelevelId";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { RolelevelId = RolelevelIdMethod });
    }

    public static RoleModel GetByRoleName(string RoleNameMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE Role_Name = @RoleName";
        return _connection.QueryFirstOrDefault<RoleModel>(sql, new { RoleName = RoleNameMethod });
    }

    public static void Update(RoleModel RoleLevel)
    {
        string sql = $"UPDATE {Table} SET Role_Name = @RoleName, Role_Level_ID = @RolelevelId WHERE id = @Id";
        _connection.Execute(sql, RoleLevel);
    }

    public static List<RoleModel> GetAllRoles()
    {
        string sql = $"SELECT * FROM {Table} ORDER BY Role_Level_ID ASC";
        List<RoleModel> roles = (List<RoleModel>)_connection.Query<RoleModel>(sql, new { });

        return roles;
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
}