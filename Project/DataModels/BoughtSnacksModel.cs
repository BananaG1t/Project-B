public class BoughtSnacksModel
{
    public Int64 Id { get; set; }
    public Int64 Account_ID { get; set; }
    public Int64 Reservation_ID { get; set; }
    public Int64 Snack_ID { get; set; }
    public int Amount { get; set; }

    public BoughtSnacksModel(Int64 id,Int64 account_id, Int64 reservationd_id, Int64 snack_id, int amount)
    {
        Id = id;
        Account_ID = account_id;
        Reservation_ID = reservationd_id;
        Snack_ID = snack_id;
        Amount = amount;
    }
    public BoughtSnacksModel(Int64 account_id, Int64 reservationd_id, Int64 snack_id, int amount)
    {
        Account_ID = account_id;
        Reservation_ID = reservationd_id;
        Snack_ID = snack_id;
        Amount = amount;
        Id = BoughtSnacksAccess.Write(this);
    }

}