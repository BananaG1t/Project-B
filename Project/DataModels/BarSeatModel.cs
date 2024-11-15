public class BarSeatModel
{
    public Int64 Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Int64 AccountId { get; set; }
    public Int64 ReservationId { get; set; }
    public Int64 SeatId { get; set; }

    public BarSeatModel(Int64 id, string startTime, string endTime, Int64 Account_ID, Int64 Reservation_ID, Int64 Seat_Number)
    {
        Id = id;

        string format = "yyyy-MM-dd HH:mm";
        DateTime.TryParseExact(startTime, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        StartTime = output;
        DateTime.TryParseExact(endTime, format, null, System.Globalization.DateTimeStyles.None, out output);
        EndTime = output;

        AccountId = Account_ID;
        ReservationId = Reservation_ID;
        SeatId = Seat_Number;
    }

    public BarSeatModel(DateTime startTime, DateTime endTime, Int64 Account_ID, Int64 Reservation_ID, Int64 Seat_Number)
    {
        StartTime = startTime;
        EndTime = endTime;
        AccountId = Account_ID;
        ReservationId = Reservation_ID;
        SeatId = Seat_Number;

        Id = BarReservationAccess.Write(this);
    }
}