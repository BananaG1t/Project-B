using Microsoft.Data.Sqlite;

using Dapper;

public static class CouponsAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
     private static string Table = "Coupons";
    
    public static Int64 Write(CouponModel coupon)
    {
        string sql = $"INSERT INTO {Table} (coupon_code,expiration_date,coupon_type,coupon_percentage,amount,Account_ID) VALUES (@CouponCode,@ExpirationDate,@CouponType,@CouponPercentage,@Amount,@AccountId)";
        _connection.Execute(sql, coupon);
        
        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }
    public static CouponModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<CouponModel>(sql, new { Id = id });
    }

    public static List<CouponModel> GetAll()
    {
        string sql = $"SELECT Account_ID,Reservation_ID,snack,amount FROM {Table}";
        List<CouponModel> Coupons = (List<CouponModel>)_connection.Query<CouponModel>(sql);

        return Coupons;
    }

    public static void Update(CouponModel coupon)
    {
        string sql = $"UPDATE {Table} SET coupon_code = @CouponCode,expiration_date = @ExpirationDate,coupon_type = @CouponType ,coupon_percentage = @CouponPercentage,amount = @Amount,AccountID = @Account_ID  WHERE id = @Id";
        _connection.Execute(sql, coupon);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }
}