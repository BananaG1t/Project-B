using Microsoft.Data.Sqlite;

using Dapper;

public static class ActiveCouponsAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
     private static readonly string Table = "ActiveCoupons";
    
    public static int Write(ActiveCouponsModel coupon)
    {
        string sql = $"INSERT INTO {Table} (Account_ID,Coupon_ID,Uses) VALUES (@AccountId,@CouponId,@Uses)";
        _connection.Execute(sql, coupon);
        
        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

        public static void Write(int account_ID, int coupon_ID, int uses)
    {
        string sql = $"INSERT INTO {Table} (Account_ID,Coupon_ID,Uses) VALUES (@AccountId,@CouponId,@Uses)";
        _connection.Execute(sql, new { AccountId = account_ID, CouponId = coupon_ID, Uses = uses });
    }
    public static ActiveCouponsModel? GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ActiveCouponsModel>(sql, new { Id = id });
    }

    public static List<ActiveCouponsModel> GetAll()
    {
        string sql = $"SELECT * FROM {Table}";
        List <ActiveCouponsModel> Coupons = (List<ActiveCouponsModel>)_connection.Query<ActiveCouponsModel>(sql);

        return Coupons;
    }

    public static void Update(ActiveCouponsModel coupon)
    {
        string sql = $"UPDATE {Table} SET Account_ID = @AccountId ,Coupon_ID = @CouponId , Uses = @Uses  WHERE id = @Id";
        _connection.Execute(sql, coupon);
    }

    public static void DeleteById(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE coupon_ID = @CouponId";
        _connection.Execute(sql, new { id = id });
    }
    public static List<ActiveCouponsModel> GetAllById(int accountId)
    {
        string sql = $"SELECT * FROM {Table} WHERE Account_ID = @Id";
        List<ActiveCouponsModel> Coupons = (List<ActiveCouponsModel>)_connection.Query<ActiveCouponsModel>(sql, new { Id = accountId});

        return Coupons;
    }

    public static int GetUses(int accountId, int couponId)
    {
        string sql = $"SELECT MAX(Uses) FROM {Table} WHERE id = @Id AND Coupon_ID = @CouponId ";
        int uses = _connection.QueryFirstOrDefault<int>(sql, new { Id = accountId, CouponId = couponId});
        return uses;
    }
}