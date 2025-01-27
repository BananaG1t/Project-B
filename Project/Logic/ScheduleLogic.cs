public static class ScheduleLogic
{

    public static ScheduleModel? GetById(int id)
    {
        return ScheduleAccess.GetById(id);
    }

    public static bool IsAvailable(int room, DateTime startTime, TimeSpan length, int locationId)
    {
        return ScheduleAccess.IsAvailable(room, startTime, startTime + length, locationId);
    }

    public static List<ScheduleModel> GetpastSchedules()
    {
        return ScheduleAccess.GetpastSchedules();
    }

    public static List<ScheduleModel> GetpastSchedulesWithMovie(MovieModel movie)
    {
        return ScheduleAccess.GetpastSchedulesWithMovie(movie);
    }

    public static int CalculateMaxIncome(int id) => CalculateMaxIncome(GetById(id) ?? throw new Exception("Schedule not found"));

    public static int CalculateMaxIncome(ScheduleModel entry)
    {
        return entry.Auditorium.Room switch
        {
            1 => 1610,
            2 => 3465,
            3 => 5750,
            _ => throw new NotImplementedException()
        };
    }

    public static List<Tuple<ScheduleModel, double>> CalculatePerSchedule(List<ScheduleModel> schedulesEntries)
    {
        List<Tuple<ScheduleModel, double>> scheduleIncome = [];
        foreach (ScheduleModel schedule in schedulesEntries)
        {
            double income = CalculateIncome(schedule);
            scheduleIncome.Add(new(schedule, income));
        }
        return scheduleIncome;
    }

    public static double CalculateIncome(int scheduleId) => CalculateIncome(ScheduleAccess.GetById(scheduleId) ?? throw new Exception("Schedule not found"));

    public static double CalculateIncome(ScheduleModel schedule)
    {
        double counter = 0;
        List<SeatModel> seats = ScheduleAccess.GetSeats(schedule);
        foreach (SeatModel seat in seats)
        {
            if (!seat.IsAvailable)
                counter += seat.Price;
        }
        return counter;
    }
    public static int EmptySeats(int scheduleId)
    {
        int counter = 0;
        ScheduleModel? entry = ScheduleAccess.GetById(scheduleId) ?? throw new Exception("Schedule not found");
        List<SeatModel> seats = ScheduleAccess.GetSeats(entry);
        foreach (SeatModel seat in seats)
        {
            if (seat.IsAvailable)
                counter += 1;
        }
        return counter;
    }
    public static int EmptySeats(ScheduleModel schedule)
    {
        int counter = 0;
        List<SeatModel> seats = ScheduleAccess.GetSeats(schedule);
        foreach (SeatModel seat in seats)
        {
            if (seat.IsAvailable)
                counter += 1;
        }
        return counter;
    }

    public static List<ScheduleModel> GetSchedules()
    {
        return ScheduleAccess.ScheduleByDate();
    }

    public static double GetIncome(List<Tuple<ScheduleModel, double>> scheduleEntries)
    {
        double total = 0;

        foreach (Tuple<ScheduleModel, double> entry in scheduleEntries)
        { total += entry.Item2; }

        return total;
    }

    public static List<IGrouping<DateTime, ScheduleModel>> GroupByDay(List<ScheduleModel> schedules)
    {
        return [.. schedules
            .GroupBy(schedule => schedule.StartTime.Date) // Group by day
            .OrderBy(group => group.Key)];
    }
}