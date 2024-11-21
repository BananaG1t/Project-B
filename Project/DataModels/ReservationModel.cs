public class ReservationModel
{
    public int Id { get; set; }
    public Int64 Account_ID { get; set; }
    public Int64 Schedule_ID { get; set; }
    public int Seat_Row { get; set; }
    public int Seat_Collum { get; set; }
    public string Status { get; set; }

    public ReservationModel(Int64 account_ID, int schedule_ID, int seat_Row, int seat_Collum, string status = "Active")
    {
        Account_ID = account_ID;
        Schedule_ID = schedule_ID;
        Seat_Row = seat_Row;
        Seat_Collum = seat_Collum;
        Status = status;
    }

    public ReservationModel(Int64 id, Int64 account_ID, Int64 schedule_ID, Int64 seat_Row, Int64 seat_Collum, string status)
    {
        Id = (int)id;
        Account_ID = account_ID;
        Schedule_ID = schedule_ID;
        Seat_Row = (int)seat_Row;
        Seat_Collum = (int)seat_Collum;
        Status = status;
    }
}



