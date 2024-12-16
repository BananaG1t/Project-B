public class OrderModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ScheduleId { get; set; }
    public int Amount { get; set; }
    public bool Bar { get; set; }

    public OrderModel(int account_ID, int schedule_ID, int amount, bool bar)
    {
        AccountId = account_ID;
        ScheduleId = schedule_ID;
        Amount = amount;
        Bar = bar;

        Id = OrderAccess.Write(this);
    }

    public OrderModel(Int64 id, Int64 Account_ID, Int64 Schedule_ID, Int64 amount, Int64 bar)
    {
        Id = (int)id;
        AccountId = (int)Account_ID;
        ScheduleId = (int)Schedule_ID;
        Amount = (int)amount;
        Bar = bar == 1;
    }

    public OrderModel(Int64 id, Int64 Account_ID, Int64 Schedule_ID, Int64 amount, string bar)
    {
        Id = (int)id;
        AccountId = (int)Account_ID;
        ScheduleId = (int)Schedule_ID;
        Amount = (int)amount;
        Bar = bar == "";
    }
}