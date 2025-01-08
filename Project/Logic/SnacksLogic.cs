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
        double totalIncome = 0;

        // Get all schedules for the last week
        DateTime oneWeekAgo = DateTime.Now.AddDays(-7);
        var weeklySchedules = ScheduleAccess.GetByDateRange(oneWeekAgo, DateTime.Now);
        
        foreach (var schedule in weeklySchedules)
        {
            var seatReservations = ReservationAcces.GetByScheduleId(schedule.Id);
            
            foreach (var seatReservation in seatReservations)
            {
                var boughtSnacks = BoughtSnacksLogic.GetAllById(seatReservation.ReservationId);
                totalIncome += CalculateIncomeForBoughtSnacks(boughtSnacks);
            }
        }
        return totalIncome;
    }


    public static double CalculateDailyIncome()
    {
        double totalIncome = 0;

        // Get all schedules for today
        DateTime today = DateTime.Now.Date;
        var dailySchedules = ScheduleAccess.GetByDateRange(today, today.AddDays(1));

        foreach (var schedule in dailySchedules)
        {
            var seatReservations = ReservationAcces.GetByScheduleId(schedule.Id);
            
            foreach (var seatReservation in seatReservations)
            {
                var boughtSnacks = BoughtSnacksLogic.GetAllById(seatReservation.ReservationId);
                totalIncome += CalculateIncomeForBoughtSnacks(boughtSnacks);
            }
        }
        return totalIncome;
    }

    public static double CalculateIncomeByMovie(MovieModel movie)
    {
        double totalIncome = 0;

        // Get all schedules for the selected movie
        var movieSchedules = ScheduleAccess.GetByMovieId((int)movie.Id);

        foreach (var schedule in movieSchedules)
        {
            var seatReservations = ReservationAcces.GetByScheduleId(schedule.Id);

            foreach (var seatReservation in seatReservations)
            {
                var boughtSnacks = BoughtSnacksLogic.GetAllById(seatReservation.ReservationId);
                totalIncome += CalculateIncomeForBoughtSnacks(boughtSnacks);
            }
        }

        return totalIncome;
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


}