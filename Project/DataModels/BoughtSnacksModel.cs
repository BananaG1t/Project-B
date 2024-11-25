public class BoughtSnacksModel
{
    public Int64 Id { get; set; }
    public Int64 Account_ID { get; set; }
    public Int64 Reservation_ID { get; set; }
    public SnacksModel Snack { get; set; }
    public int Amount { get; set; }

    public BoughtSnacksModel(Int64 id,Int64 account_id, Int64 reservationd_id, SnacksModel snack, int amount)
    {
        Id = id;
        Account_ID = account_id;
        Reservation_ID = reservationd_id;
        Snack = snack;
        Amount = amount;
    }
    public BoughtSnacksModel(Int64 account_id, Int64 reservationd_id, SnacksModel snack, int amount)
    {
        Id = BoughtSnacksAccess.Write(this);
        Account_ID = account_id;
        Reservation_ID = reservationd_id;
        Snack = snack;
        Amount = amount;
    }

}