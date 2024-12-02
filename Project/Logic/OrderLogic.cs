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
            ScheduleModel schedule = ScheduleLogic.GetById(order.Schedule_ID);
            text += $"\n[{order.Id}] Movie: {schedule.Movie.Name}, Date: {schedule.StartTime}, Seats: {order.Amount}, Bar: {order.Bar}";
            valid.Add(order.Id);
        }
        int answer = General.ValidAnswer(text, valid);
        return orders.First(OrderModel => OrderModel.Id == answer);
    }
}