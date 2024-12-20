using Microsoft.Data.Sqlite;

using Dapper;

public static class CouponsAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
     private static string Table = "Coupons";
    
    public static int Write(CouponModel coupon)
    {
        string sql = $"INSERT INTO {Table} (coupon_code,expiration_date,coupon_type,coupon_percentage,amount,Account_ID) VALUES (@CouponCode,@ExpirationDate,@CouponType,@CouponPercentage,@Amount,@AccountId)";
        _connection.Execute(sql, coupon);
        
        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }
    public static CouponModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<CouponModel>(sql, new { Id = id });
    }

    public static List<CouponModel> GetAll()
    {
        string sql = $"SELECT * FROM {Table}";
        List <CouponModel> Coupons = (List<CouponModel>)_connection.Query<CouponModel>(sql);

        return Coupons;
    }
        public static List<CouponModel> GetAllById(int accountId)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_ID = @AccountId";
        List<CouponModel> Coupons = (List<CouponModel>)_connection.Query<CouponModel>(sql, new { AccountId = accountId});
        
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
            public static List<CouponModel> GetAllByAccountId(int accountId, string couponType)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_ID = @Id AND Coupon_Type = @CouponType";
        List<CouponModel> Coupons = (List<CouponModel>)_connection.Query<CouponModel>(sql, new { Id = accountId, CouponType = couponType});

        return Coupons;
    }
}