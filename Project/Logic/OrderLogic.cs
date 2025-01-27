class OrderLogic
{
    public OrderLogic()
    {

    }

    public static int Write(OrderModel order)
    {
        return OrderAccess.Write(order);
    }

    public static OrderModel? GetById(int id)
    {
        return OrderAccess.GetById(id);
    }

    public static List<OrderModel> GetFromAccount(AccountModel account)
    {
        return OrderAccess.GetFromAccount(account);
    }

    public static bool CheckBarSeats(ScheduleModel schedule, int seatAmount)
    {
        try
        {
            List<(int, DateTime, int)> groupedBarSpots = OrderAccess.GetAvailableBarSpots(schedule);
            if (40 - MinimizeSpots(groupedBarSpots) >= seatAmount)
                return true;
            else return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    static int MinimizeSpots(List<(int id, DateTime startTime, int amount)> reservations)
    {
        // Sort reservations by startTime
        reservations = [.. reservations.OrderBy(r => r.startTime)];

        // Priority queue (min-heap) to track active reservations (endTime, amount)
        var activeReservations = new SortedSet<(DateTime endTime, int amount)>();

        foreach (var reservation in reservations)
        {
            DateTime currentStartTime = reservation.startTime;
            DateTime currentEndTime = currentStartTime.AddHours(2);

            // Remove expired reservations (those whose endTime <= current startTime)
            int amount = activeReservations.Where(res => res.endTime <= currentStartTime).Sum(res => res.amount);
            activeReservations.RemoveWhere(res => res.endTime <= currentStartTime);

            // Add current reservation to the active set
            activeReservations.Add((currentEndTime, Math.Max(reservation.amount, amount)));
        }

        // Calculate total spots in use and return it
        return activeReservations.Sum(res => res.amount); ;
    }
}