using Microsoft.Data.Sqlite;

using Dapper;
public class Access<T>
{
    protected SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    public void Delete(int id, string tableName)
    {
        string sql = $"DELETE FROM {tableName} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
    public T? GetById(int id, string tableName)
    {
        string sql = $"SELECT * FROM {tableName} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<T>(sql, new { Id = id });
    }
    public List<T> GetAll(string tableName)
    {
        string sql = $"SELECT * FROM {tableName}";
        return _connection.Query<T>(sql).ToList();
    }
}