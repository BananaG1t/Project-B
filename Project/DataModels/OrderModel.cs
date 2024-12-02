public class OrderModel
{
    public int Id { get; set; }
    public int Account_ID { get; set; }
    public int Schedule_ID { get; set; }
    public int Amount { get; set; }
    public bool Bar { get; set; }

    public OrderModel(int account_ID, int schedule_ID, int amount, bool bar)
    {
        Account_ID = account_ID;
        Schedule_ID = schedule_ID;
        Amount = amount;
        Bar = bar;

        Id = OrderAccess.Write(this);
    }

    public OrderModel(Int64 id, Int64 account_ID, Int64 schedule_ID, Int64 amount, Int64 bar)
    {
        Id = (int)id;
        Account_ID = (int)account_ID;
        Schedule_ID = (int)schedule_ID;
        Amount = (int)amount;
        Bar = bar == 1;
    }
}