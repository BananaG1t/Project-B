public class ReservationModel
{
    private int row;
    private int collum;

    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Seat_Row { get; set; }
    public int Seat_Collum { get; set; }
    public string Status { get; set; }

    public ReservationModel(int orderId, int seat_Row, int seat_Collum, string status = "Active")
    {
        OrderId = orderId;
        Seat_Row = seat_Row;
        Seat_Collum = seat_Collum;
        Status = status;
    }

    public ReservationModel(Int64 id, Int64 Order_ID, Int64 seat_Row, Int64 seat_Collum, string status)
    {
        Id = (int)id;
        OrderId = (int)Order_ID;
        Seat_Row = (int)seat_Row;
        Seat_Collum = (int)seat_Collum;
        Status = status;
    }
}



