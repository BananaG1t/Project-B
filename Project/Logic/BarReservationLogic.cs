public static class BarReservationLogic
{
    public static bool ReserveBarSeats(AccountModel account, ScheduleModel schedule, int seatAmount, int reservationId)
    {
        List<int> availableBarSeats = AvailableBarSeats(schedule.EndTime);
        if (!BarAvailable(availableBarSeats, seatAmount)) { return false; }
        for (int i = 0; i < seatAmount; i++)
        {
            TimeSpan length = new TimeSpan(2, 0, 0);
            BarSeatModel barSeat = new(schedule.EndTime, schedule.EndTime + length, account.Id, reservationId, availableBarSeats[i]);
        }
        return true;
    }

    public static List<int> AvailableBarSeats(DateTime starttime)
    {
        TimeSpan length = new TimeSpan(2, 0, 0);
        return BarReservationAccess.IsAvailable(starttime, starttime + length);
    }

    public static bool BarAvailable(List<int> availableBarSeats, int seatAmount) => availableBarSeats.Count() >= seatAmount;
}