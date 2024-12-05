public static class BarReservationLogic
{
    public static bool ReserveBarSeats(AccountModel account, ScheduleModel schedule, int seatAmount, int reservationId)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }

    }

    public static List<int> AvailableBarSeats(DateTime starttime)
    {
        List<int> validSeats = [];

        TimeSpan length = new TimeSpan(2, 0, 0);

        for (int seatNumber = 1; seatNumber < 41; seatNumber++)
        {
            if (BarReservationAccess.IsAvailable(seatNumber, starttime, starttime + length))
                validSeats.Add(seatNumber);
        }

        return validSeats;
    }

    public static bool BarAvailable(List<int> availableBarSeats, int seatAmount) => availableBarSeats.Count() >= seatAmount;
}