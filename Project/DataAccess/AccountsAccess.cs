using Microsoft.Data.Sqlite;

using Dapper;


public static class AccountsAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static readonly string Table = "Accounts";

    public static int Write(AccountModel account)
    {
        string sql = $"INSERT INTO {Table} (email, password, fullname) VALUES (@EmailAddress, @Password, @FullName)";
        _connection.Execute(sql, account);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

    public static void Update(AccountModel account)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        _connection.Execute(sql, account);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

    public static AccountModel? GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Id = id });
    }

    public static AccountModel? GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public static List<AccountModel> GetAllAccounts()
    {
        string sql = $"SELECT * FROM {Table} ORDER BY id ASC";
        return (List<AccountModel>)_connection.Query<AccountModel>(sql, new { });
    }

}