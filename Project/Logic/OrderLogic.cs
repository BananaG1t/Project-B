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
        int answer = PresentationHelper.MenuLoop(text, valid);
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