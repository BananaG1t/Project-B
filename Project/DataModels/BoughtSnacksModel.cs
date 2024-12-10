public class BoughtSnacksModel
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public int SnackId { get; set; }
    public int Amount { get; set; }

    public BoughtSnacksModel(Int64 id, Int64 Reservation_ID, Int64 Snack_ID, int amount)
    {
        Id = (int)id;
        ReservationId = (int)Reservation_ID;
        SnackId = (int)Snack_ID;
        Amount = amount;
    }
    public BoughtSnacksModel(int reservation_id, int snack_id, int amount)
    {
        ReservationId = reservation_id;
        SnackId = snack_id;
        Amount = amount;
        
        Id = BoughtSnacksAccess.Write(this);
    }

}