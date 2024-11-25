using Microsoft.Data.Sqlite;

using Dapper;


public static class BoughtSnacksAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
     private static string Table = "Bought_snacks";
    
    public static Int64 Write(BoughtSnacksModel snack)
    {
        string sql = $"INSERT INTO {Table} (Account_ID,Reservation_ID,snack,amount) VALUES (@Account_Id,@Reservation_Id,@Snack,@Amount)";
        _connection.Execute(sql, snack);
        
        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }
    public static BoughtSnacksModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<BoughtSnacksModel>(sql, new { Id = id });
    }

    public static List<BoughtSnacksModel> GetAll()
    {
        string sql = $"SELECT Account_ID,Reservation_ID,snack,amount FROM {Table}";
        List<BoughtSnacksModel> Snacks = (List<BoughtSnacksModel>)_connection.Query<BoughtSnacksModel>(sql);

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