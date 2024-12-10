static class Order
{
    public static OrderModel SelectOrder(AccountModel account)
    {
        return OrderLogic.SelectOrder(account);
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