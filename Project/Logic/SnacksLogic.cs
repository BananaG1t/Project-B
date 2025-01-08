public static class SnacksLogic
{
    public static void Add(string name, double price)
    {
        new SnacksModel(name, price);
    }

    public static SnacksModel GetById(int id)
    {
        return SnacksAccess.GetById(id);
    }

    public static List<SnacksModel> GetAll()
    {
        return SnacksAccess.GetAll();
    }

    public static void update(SnacksModel snack)
    {
        SnacksAccess.Update(snack);
    }
    public static void Delete(int id)
    {
        SnacksAccess.Delete(id);
    }

    private static List<SnacksModel> SnacksModel = new List<SnacksModel>
    {

    };

    public static double CalculateWeeklyIncome()
    {
        // Get all schedules for the last week
        DateTime oneWeekAgo = DateTime.Now.AddDays(-7);
        DateTime oneWeekInfront = DateTime.Now.AddDays(7);
        List<ScheduleModel> weeklySchedules = ScheduleAccess.GetByDateRange(oneWeekAgo, oneWeekInfront);

        return CalculateIncomeBySchedules(weeklySchedules);
    }


    public static double CalculateDailyIncome()
    {
        double totalIncome = 0;

        // Get all schedules for today
        DateTime today = DateTime.Now.Date;
        List<ScheduleModel> dailySchedules = ScheduleAccess.GetByDateRange(today, today.AddDays(1));

        return CalculateIncomeBySchedules(dailySchedules);
    }

    public static double CalculateIncomeByMovie(MovieModel movie)
    {
        double totalIncome = 0;

        // Get all schedules for the selected movie
        List<ScheduleModel> movieSchedules = ScheduleAccess.GetByMovieId((int)movie.Id);

        return CalculateIncomeBySchedules(movieSchedules);
    }

    private static double CalculateIncomeForBoughtSnacks(List<BoughtSnacksModel> boughtSnacks)
    {
        double total = 0;

        foreach (var boughtSnack in boughtSnacks)
        {
            SnacksModel snack = GetById(boughtSnack.SnackId);
            total += boughtSnack.Amount * snack.Price;
        }

        return total;
    }


    public static double CalculateIncomeByReservation(BoughtSnacksModel boughtSnack)
    {
        double total = 0;
        List<BoughtSnacksModel> boughtSnacks = BoughtSnacksLogic.GetAllById(boughtSnack.ReservationId);
        if (boughtSnacks.Count == 0) return 0;
        foreach (BoughtSnacksModel currentSnack in boughtSnacks)
        {
            SnacksModel snack = GetById(currentSnack.SnackId);
            total += currentSnack.Amount * snack.Price;
        }
        return total;
    }

    public static double CalculateIncomeBySchedules(List<ScheduleModel> schedules)
    {
        double totalIncome = 0;
        foreach (ScheduleModel schedule in schedules)
        {
            List<OrderModel> orders = OrderAccess.GetFromSchedule(schedule);
            foreach (OrderModel order in orders)
            {
                List<ReservationModel> reservations = ReservationAcces.GetFromOrder(order);
                foreach (ReservationModel reservation in reservations)
                {
                    List<BoughtSnacksModel> boughtSnacks = BoughtSnacksAccess.GetByReservationId(reservation.Id);
                    foreach (BoughtSnacksModel currentSnack in boughtSnacks)
                    {
                        SnacksModel snack = GetById(currentSnack.SnackId);
                        totalIncome += currentSnack.Amount * snack.Price;
                    }
                }
            }
        }

        return totalIncome;
    }
}