static class BarReservationLogic
{
    public static bool ReserveBarSeats(AccountModel account, ScheduleModel schedule, int seatAmount)
    {
        if (!BarAvailable(seatAmount, schedule)) { return false; }
        for (int i = 0; i < seatAmount; i++)
        {
            // BarReservationAccess.Write()
        }
        return true;
    }

    private static bool BarAvailable(int seatAmount, ScheduleModel schedule)
    {
        return AvailableBarSeats(seatAmount, schedule.EndTime).Count() >= seatAmount;
    }

    private static Dictionary<int, bool> AvailableBarSeats(int seatAmount, DateTime starttime)
    {
        Dictionary<int, bool> availableSeatsDict = [];
        int AvailableSeats = 0;
        string endtime = "";
        TimeSpan length = new TimeSpan(2, 0, 0);
        for (int i = 0; i < 40; i++)
        {
            if (BarReservationAccess.IsAvailable(i, starttime, starttime + length))
            {
                availableSeatsDict.Add(i, true);
                AvailableSeats++;
            }
        }
        return availableSeatsDict;
    }
}