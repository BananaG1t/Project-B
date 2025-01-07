//This class is not static so later on we can use inheritance and interfaces
public class SeatLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static SeatModel? CurrentSeat { get; private set; }

    public SeatLogic()
    {
        // Could do something here
    }

    public static SeatModel GetByReservationInfo(int ColNum, int RowNum, int Auditorium_ID)
    {
        return SeatsAccess.GetByReservationInfo(ColNum, RowNum, Auditorium_ID);
    }
    public static SeatModel GetByReservation(int ColNum, int RowNum)
    {
        return SeatsAccess.GetByReservation(ColNum, RowNum);
    }
    public static void Update(SeatModel seat)
    {
        SeatsAccess.Update(seat);
    }
}


