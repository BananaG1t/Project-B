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

        for (int i = 0; i < orders.Count; i++)
        {
            ScheduleModel? schedule = ScheduleLogic.GetById(orders[i].ScheduleId);
            if (schedule == null) { continue; }
            text += $"\n[{i +1}] Movie: {schedule.Movie.Name}, Date: {schedule.StartTime}, Seats: {orders[i].Amount}, Bar: {orders[i].Bar}";
        }

        text += "\n[0] Go back";

        int answer = PresentationHelper.MenuLoop(text, 0, orders.Count);

        if (answer == 0) { return null; }
        return orders[answer - 1];
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