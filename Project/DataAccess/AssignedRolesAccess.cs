using Microsoft.Data.Sqlite;

using Dapper;

public static class AssignedRoleAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static readonly string Table = "AssignedRoles";

    public static int Write(AssignedRoleModel AssignedRoleModel)
    {
        string sql = $"INSERT INTO {Table} (Role, Account_ID, Location_ID) VALUES (@RoleId, @AccountId, @LocationId)";
        _connection.Execute(sql, AssignedRoleModel);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

    public static void Update(AssignedRoleModel AssignedRoleModel)
    {
        string sql = $"UPDATE {Table} SET Role = @RoleId, Account_ID = @AccountId, Location_ID = @LocationId WHERE id = @Id";
        _connection.Execute(sql, AssignedRoleModel);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static bool IsAvailable(int roleId, int? locationId, int accountId)
    {
        string sql = @$"
                SELECT COUNT(*) FROM {Table} 
                WHERE Role = @RoleId 
                AND Account_ID = @AccountId 
                AND Location_ID = @LocationId";
        return _connection.ExecuteScalar<int>(sql, new { RoleId = roleId, LocationId = locationId, AccountId = accountId }) == 0;
    }

    public static AssignedRoleModel? GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<AssignedRoleModel>(sql, new { Id = id });
    }

    public static AssignedRoleModel? GetByRoleId(int RoleIdMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE Role = @RoleId";
        return _connection.QueryFirstOrDefault<AssignedRoleModel>(sql, new { RoleId = RoleIdMethod });
    }

    public static AssignedRoleModel? GetByAccountId(int AccountIdMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_ID = @AccountId";
        return _connection.QueryFirstOrDefault<AssignedRoleModel>(sql, new { AccountId = AccountIdMethod });
    }

    public static AssignedRoleModel? GetByLocationId(int LocationIdMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE Location_ID = @Id";
        return _connection.QueryFirstOrDefault<AssignedRoleModel>(sql, new { Id = LocationIdMethod });
    }

    public static List<AssignedRoleModel> GetAllByLocationId(int LocationIdMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE Location_ID = @Id";
        return (List<AssignedRoleModel>)_connection.Query<AssignedRoleModel>(sql, new { Id = LocationIdMethod });
    }

    public static List<AssignedRoleModel> GetAllAssignedRoles()
    {
        string sql = $"SELECT * FROM {Table} ORDER BY Account_ID ASC";
        return (List<AssignedRoleModel>)_connection.Query<AssignedRoleModel>(sql, new { });
    }

    public static List<AssignedRoleModel> GetAllByAccountId(int AccountIdMethod)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_ID = @AccountId";
        return (List<AssignedRoleModel>)_connection.Query<AssignedRoleModel>(sql, new { AccountId = AccountIdMethod });
    }
}