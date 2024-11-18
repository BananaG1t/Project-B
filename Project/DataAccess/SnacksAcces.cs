using Microsoft.Data.Sqlite;

using Dapper;


public static class SnacksAcces
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
     private static string Table = "Available_snacks";
    
    public static void Write(SnacksModel snack)
    {
        string sql = $"INSERT INTO {Table} (name,price) VALUES (@Name,@Price)";
        _connection.Execute(sql, snack);
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

    public static void Delete(string snackName)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new {  name = snackName });
    }
}