using Microsoft.Data.Sqlite;

using Dapper;


public static class SnacksAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
     private static string Table = "Available_snacks";
    
    public static  Int64 Write(SnacksModel snack)
    {
        string sql = $"INSERT INTO {Table} (name,price) VALUES (@Name,@Price)";
        _connection.Execute(sql, snack);
        
        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }
    public static SnacksModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<SnacksModel>(sql, new { Id = id });
    }

    public static void Update(SnacksModel snack)
    {
        string sql = $"UPDATE {Table} SET status = @Status WHERE id = @Id";
        _connection.Execute(sql, snack);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
}