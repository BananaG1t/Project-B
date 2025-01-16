using Microsoft.Data.Sqlite;

using Dapper;


public static class SnacksAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private static string Table = "Snacks";

    public static int Write(SnacksModel snack)
    {
        string sql = $"INSERT INTO {Table} (name,price) VALUES (@Name,@Price)";
        _connection.Execute(sql, snack);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }
    public static SnacksModel? GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<SnacksModel>(sql, new { Id = id });
    }



    public static List<SnacksModel> GetAll()
    {
        string sql = $"SELECT id, name, price FROM {Table}";
        List<SnacksModel> Snacks = (List<SnacksModel>)_connection.Query<SnacksModel>(sql);

        return Snacks;
    }

    public static void Update(SnacksModel snack)
    {
        string sql = $"UPDATE {Table} SET name = @Name, price = @Price WHERE id = @Id";
        _connection.Execute(sql, snack);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
}