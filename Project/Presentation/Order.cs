static class Order
{
    public static OrderModel? SelectOrder(AccountModel account)
    {
        Console.Clear();
        List<OrderModel> orders = OrderLogic.GetFromAccount(account);
        if (!orders.Any())
        {
            PresentationHelper.Error("No orders available.");
            return null;
        }
        
        string text = "What order do you want to manage?";
        
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

    public static void GetBarReservation(ScheduleModel schedule, int SeatAmount)
    {
        string text = $"Do you want to sit at the bar after the movie?\n[1] Yes\n[2] No";

        bool choice = PresentationHelper.MenuLoop(text, 1, 2) == 1;
        if (!choice) { Console.Clear(); return; }
        if (OrderLogic.CheckBarSeats(schedule, SeatAmount))
        {
            // Console.Clear();
            Console.WriteLine($"You have booked {SeatAmount} seats for {schedule.EndTime}");
            return;
        }
        else
        {
            Console.WriteLine($"There was not enough space to book {SeatAmount} seats");
        }
        ;
    }
}