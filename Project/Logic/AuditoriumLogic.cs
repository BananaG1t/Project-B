//This class is not static so later on we can use inheritance and interfaces
public class AuditoriumLogic
{

    public AuditoriumLogic()
    {
        // Could do something here

    }

    public static Dictionary<(int Row, int Collum), SeatModel> CreateSeats(Int64 id, int room_id)
    {
        Dictionary<(int Row, int Collum), SeatModel> Seats = [];
        List<AuditoriumLayoutModel> layout = AuditoriumLayoutAccess.GetById(room_id);
        foreach (AuditoriumLayoutModel seat in layout)
        {
            Seats[(seat.Row, seat.Collum)] = new SeatModel(id, seat.Row, seat.Collum, seat.Class, seat.Price);
        }

        return Seats;
    }

    public static Dictionary<(int Row, int Collum), SeatModel> GetSeats(Int64 id)
    {
        Dictionary<(int Row, int Collum), SeatModel> Seats = [];
        List<SeatModel> seats = SeatsAccess.GetByRoom((int)id);
        foreach (SeatModel seat in seats)
        {
            Seats[(seat.Row, seat.Collum)] = seat;
        }

        return Seats;
    }
}
