class OrderLogic
{
    public OrderLogic()
    {

    }

    public static OrderModel SelectOrder(AccountModel account)
    {
        Console.Clear();
        string text = "What order do you want to manage?";
        List<OrderModel> orders = OrderAccess.GetFromAccount(account);
        List<int> valid = [];

        foreach (OrderModel order in orders)
        {
            ScheduleModel schedule = ScheduleLogic.GetById(order.ScheduleId);
            text += $"\n[{order.Id}] Movie: {schedule.Movie.Name}, Date: {schedule.StartTime}, Seats: {order.Amount}, Bar: {order.Bar}";
            valid.Add(order.Id);
        }
        int answer = General.ValidAnswer(text, valid);
        return orders.First(OrderModel => OrderModel.Id == answer);
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
        reservations = reservations.OrderBy(r => r.startTime).ToList();

        // Priority queue (min-heap) to track active reservations (endTime, amount)
        var activeReservations = new SortedSet<(DateTime endTime, int amount)>();

        int maxSpots = 0;

        foreach (var reservation in reservations)
        {
            DateTime currentStartTime = reservation.startTime;
            DateTime currentEndTime = currentStartTime.AddHours(2);

            // Remove expired reservations (those whose endTime <= current startTime)
            activeReservations.RemoveWhere(res => res.endTime <= currentStartTime);

            // Add current reservation to the active set
            activeReservations.Add((currentEndTime, reservation.amount));

            // Calculate total spots in use
            int currentSpotsInUse = activeReservations.Sum(res => res.amount);

            // Track the maximum number of spots required
            maxSpots = Math.Max(maxSpots, currentSpotsInUse);
        }

        return maxSpots;
    }
}