public class ReservationModel
{
    public Int64 Account_ID { get; set; }
    public int Schedule_ID { get; set; }
    public int Seat_Row { get; set; }
    public int Seat_Collum { get; set; }
    public string Status { get; set; }
    public ReservationModel()
    {

    }

    public ReservationModel(Int64 account_ID, int schedule_ID, int seat_Row, int seat_Collum, string status)
    {
        Account_ID = account_ID;
        Schedule_ID = schedule_ID;
        Seat_Row = seat_Row;
        Seat_Collum = seat_Collum;
        Status = status;
    }
}



