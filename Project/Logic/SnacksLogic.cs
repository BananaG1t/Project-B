public static class SnacksLogic
{
    public static void Add(string name, double price)
    {
        new SnacksModel(name, price);
    }

    public static SnacksModel? GetById(int id)
    {
        return SnacksAccess.GetById(id);
    }

    public static List<SnacksModel> GetAll()
    {
        return SnacksAccess.GetAll();
    }

    public static void Update(SnacksModel snack)
    {
        SnacksAccess.Update(snack);
    }
    public static void Delete(int id)
    {
        SnacksAccess.Delete(id);
    }
    
    public static void Delete(SnacksModel snack)
    {
        SnacksAccess.Delete(snack.Id);
    }


    public static double CalculateWeeklyIncome()
    {
        // Get all schedules for the last week
        DateTime oneWeekAgo = DateTime.Now.AddDays(-7);

        List<ScheduleModel> weeklySchedules = ScheduleAccess.GetByDateRange(oneWeekAgo, DateTime.Now);

        return CalculateIncomeBySchedules(weeklySchedules);
    }


    public static double CalculateDailyIncome()
    {
        // Get all schedules for today
        DateTime today = DateTime.Now.Date;
        List<ScheduleModel> dailySchedules = ScheduleAccess.GetByDateRange(today, today.AddDays(1));

        return CalculateIncomeBySchedules(dailySchedules);
    }

    public static double CalculateIncomeByMovie(MovieModel movie)
    {
        // Get all schedules for the selected movie
        List<ScheduleModel> movieSchedules = ScheduleAccess.GetByMovieId((int)movie.Id);

        return CalculateIncomeBySchedules(movieSchedules);
    }

    private static double CalculateIncomeForBoughtSnacks(List<BoughtSnacksModel> boughtSnacks)
    {
        double total = 0;

        foreach (var boughtSnack in boughtSnacks)
        {
            SnacksModel snack = GetById(boughtSnack.SnackId) ?? throw new Exception("Snack not found");
            total += boughtSnack.Amount * snack.Price;
        }

        return total;
    }


    public static double CalculateIncomeByReservation(BoughtSnacksModel boughtSnack)
    {
        double total = 0;
        List<BoughtSnacksModel> boughtSnacks = BoughtSnacksLogic.GetAllById(boughtSnack.ReservationId);
        CouponModel? coupon;
        coupon = CouponsAccess.GetBySnack(boughtSnack);
        if (boughtSnacks.Count == 0) return 0;
        foreach (BoughtSnacksModel currentSnack in boughtSnacks)
        {
            SnacksModel snack = GetById(currentSnack.SnackId) ?? throw new Exception("Snack not found"); // can't be null due to foreign key constraint
            if (coupon is not null && (coupon.CouponType == "Snacks" || coupon.CouponType == "Order"))
                total += CouponsLogic.DiscountPrice(currentSnack.Amount * snack.Price, coupon);
            else
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
                CouponModel? coupon = null;
                if (order.CouponId is not null)
                    coupon = CouponsAccess.GetById((int)order.CouponId);
                List<ReservationModel> reservations = ReservationAcces.GetFromOrder(order);
                foreach (ReservationModel reservation in reservations)
                {
                    List<BoughtSnacksModel> boughtSnacks = BoughtSnacksAccess.GetByReservationId(reservation.Id);
                    foreach (BoughtSnacksModel currentSnack in boughtSnacks)
                    {
                        SnacksModel snack = GetById(currentSnack.SnackId) ?? throw new Exception("Snack not found"); // can't be null due to foreign key constraint
                        if (coupon is not null && (coupon.CouponType == "Snacks" || coupon.CouponType == "Order"))
                        {
                            totalIncome += CouponsLogic.DiscountPrice(currentSnack.Amount * snack.Price, coupon);
                        }
                        else
                        {
                            totalIncome += currentSnack.Amount * snack.Price;
                        }
                    }
                }
            }
        }
        return totalIncome;
    }
}