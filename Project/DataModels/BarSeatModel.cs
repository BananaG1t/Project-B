public class BarSeatModel
{
    public Int64 Id { get; set; }
    public Int64 SeatId { get; set; }
    public Int64 ReservationId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public BarSeatModel(Int64 reservationId, string startTime, string endTime)
    {
        string format = "yyyy-MM-dd HH:mm:ss";

        ReservationId = reservationId;
        DateTime.TryParseExact(startTime, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        StartTime = output;
        DateTime.TryParseExact(endTime, format, null, System.Globalization.DateTimeStyles.None, out output);
        EndTime = output;
        Id = BarReservationAccess.Write(this);
    }
}