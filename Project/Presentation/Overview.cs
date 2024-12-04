static class Overview
{
    public static void MoneyOverview()
    {
        Console.Clear();
        string text = "Welcome to the income overview\n" +
        "[1] Show income from al movies\n" +
        "[2] Show movie income from certain schedule entry\n" +
        "[3] Show income from movie";
        int input = PresentationHelper.MenuLoop(text, 1, 3);

        switch (input)
        {
            case 1:
                TotalIncome();
                break;
            case 2:
                ScheduleIncome();
                break;
            case 3:
                TotalMovieIncome();
                break;
        }
        Menu.AdminMenu();
    }

    public static void TotalIncome()
    {
        Console.Clear();
        List<Tuple<ScheduleModel, double>> scheduleEntries = ScheduleLogic.CalculatePerSchedule(ScheduleLogic.GetpastSchedules());

        foreach (Tuple<ScheduleModel, double> entry in scheduleEntries)
        {
            Console.WriteLine($"Schedule: {entry.Item1.Id}, Income: {entry.Item2}, Empty seats {ScheduleLogic.EmptySeats(entry.Item1)}");
        }

        Console.WriteLine($"\nTotal income: {ScheduleLogic.GetIncome(scheduleEntries)}");
    }

    public static void ScheduleIncome()
    {
        Console.Clear();
        string text = "From which schedule would you like to see the income";

        List<ScheduleModel> pastEntries = ScheduleLogic.GetpastSchedules();

        foreach (ScheduleModel entry in pastEntries)
        {
            text += $"\n[{entry.Id}] room: {entry.Auditorium.Room}, movie: {entry.Movie.Name}, date: {entry.StartTime}";
        }

        int input = PresentationHelper.MenuLoop(text, 1, pastEntries.Count);

        ScheduleModel Selected = pastEntries.First(ScheduleModel => ScheduleModel.Id == input);

        Console.WriteLine($"income: {ScheduleLogic.CalculateIncome(Selected)} / {ScheduleLogic.CalculateMaxIncome(Selected)}");
        Console.WriteLine($"Amount of empty seats: {ScheduleLogic.EmptySeats(Selected)}");
    }


    public static void TotalMovieIncome()
    {
        Console.Clear();
        string text = "What movie do you want to show?";

        List<MovieModel> Movies = MovieLogic.GetAll();

        foreach (MovieModel movie in Movies)
        {
            text += $"\n[{movie.Id}] {movie.Name}";
        }

        int input = PresentationHelper.MenuLoop(text, 1, Movies.Count);

        MovieModel Selected = Movies.First(MovieModel => MovieModel.Id == input);

        List<ScheduleModel> entryWithMovie = ScheduleLogic.GetpastSchedulesWithMovie(Selected);
        List<Tuple<ScheduleModel, double>> scheduleEntries = ScheduleLogic.CalculatePerSchedule(entryWithMovie);

        foreach (Tuple<ScheduleModel, double> entry in scheduleEntries)
        {
            Console.WriteLine($"Schedule: {entry.Item1.Id}, Income: {entry.Item2}, Empty seats {ScheduleLogic.EmptySeats(entry.Item1)}");
        }
        Console.WriteLine($"\nTotal income: {ScheduleLogic.GetIncome(scheduleEntries)}");
    }
}