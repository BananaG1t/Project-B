public class OrderModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ScheduleId { get; set; }
    public int Amount { get; set; }
    public bool Bar { get; set; }
    public int? CouponId { get; set; }

    public OrderModel(int account_ID, int schedule_ID, int amount, bool bar, int? coupon_ID)
    {
        AccountId = account_ID;
        ScheduleId = schedule_ID;
        Amount = amount;
        Bar = bar;
        CouponId = coupon_ID;

        Id = OrderAccess.Write(this);
    }

    public OrderModel(Int64 id, Int64 Account_ID, Int64 Schedule_ID, Int64 amount, Int64 bar, Int64? Coupon_ID)
    {
        Id = (int)id;
        AccountId = (int)Account_ID;
        ScheduleId = (int)Schedule_ID;
        Amount = (int)amount;
        Bar = bar == 1;
        CouponId = (int?)Coupon_ID;
    }

    public OrderModel(Int64 id, Int64 Account_ID, Int64 Schedule_ID, Int64 amount, string bar, Int64? Coupon_ID)
    {
        // for dapper when it tries to get a bool value from the database with no entry it returns a string
    }
}