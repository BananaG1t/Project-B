public static class BarReservationLogic
{
    public static bool ReserveBarSeats(AccountModel account, ScheduleModel schedule, int seatAmount)
    {
        List<int> availableBarSeats = AvailableBarSeats(schedule.EndTime);
        if (!BarAvailable(availableBarSeats, seatAmount)) { return false; }
        for (int i = 0; i < seatAmount; i++)
        {
            // BarReservationAccess.Write()
        }
        return true;
    }

    public static List<int> AvailableBarSeats(DateTime starttime)
    {
        List<int> availableSeats = [];
        TimeSpan length = new TimeSpan(2, 0, 0);
        for (int i = 0; i < 40; i++)
        {
            if (BarReservationAccess.IsAvailable(i, starttime, starttime + length))
            {
                availableSeats.Add(i);
            }
        }
        return availableSeats;
    }

    public static bool BarAvailable(List<int> availableBarSeats, int seatAmount) => availableBarSeats.Count() >= seatAmount;

}