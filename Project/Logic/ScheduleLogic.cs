public class ScheduleLogic
{

    public ScheduleLogic()
    {
        // Could do something here

    }

    public static bool IsAvailable(int room, DateTime startTime, TimeSpan length)
    {
        return ScheduleAccess.IsAvailable(room, startTime, startTime + length);
    }

    public int CalculateMaxIncome(int id)
    {
        return id switch
        {
            1 => 1610,
            2 => 3465,
            3 => 5750
        };
    }

    public List<Tuple<ScheduleModel, double>> CalculatePerSchedule()
    {
        List<Tuple<ScheduleModel, double>> scheduleIncome = [];
        List<ScheduleModel> schedulesEntries = ScheduleAccess.GetpastSchedules();
        foreach(ScheduleModel schedule in schedulesEntries)
        {
            double income = CalculateIncome(schedule);
            scheduleIncome.Add(new (schedule,income));
        }
        return scheduleIncome;
    }

    public double CalculateIncome(int scheduleId)
    {
        double counter = 0;
        ScheduleModel entry = ScheduleAccess.GetById(scheduleId);
        List<SeatModel> seats = ScheduleAccess.GetSeats(entry);
        foreach (SeatModel seat in seats)
        {
            if (!seat.IsAvailable)
                counter += seat.Price;
        }
        return counter;
    }
        public double CalculateIncome(ScheduleModel schedule)
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
        public int EmptySeats(int scheduleId)
    {
        int counter = 0;
        ScheduleModel entry = ScheduleAccess.GetById(scheduleId);
        List<SeatModel> seats = ScheduleAccess.GetSeats(entry);
        foreach (SeatModel seat in seats)
        {
            if (seat.IsAvailable)
                counter += 1;
        }
        return counter;
    }
        public int EmptySeats(ScheduleModel schedule)
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
}