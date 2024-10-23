static class Overview
{
    static ScheduleLogic Logic = new();
    public static void MoneyOverview()
    {
        Console.Clear();
        string text = "Welcome to the income overview\n" +
        "[1] Show total income\n" +
        "[2] Show income from certain schedule entry\n" +
        "[3] Show income from movie";
        int Choice = General.ValidAnswer(text, [1, 2, 3]);

        switch (Choice)
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
        List<Tuple<ScheduleModel, double>> scheduleEntries = Logic.CalculatePerSchedule(ScheduleLogic.GetpastSchedules());
        double cnt = 0;
        foreach (Tuple<ScheduleModel, double> entry in scheduleEntries)
        {
            Console.WriteLine($"Schedule: {entry.Item1.Id}, Income: {entry.Item2}, Empty seats {Logic.EmptySeats(entry.Item1)}");
            cnt += entry.Item2;
        }
        Console.WriteLine($"\nTotal income: {cnt}");
    }

    public static void ScheduleIncome()
    {
        Console.Clear();
        string text = "From which schedule would you like to see the income";
        List<int> valid = [];
        List<ScheduleModel> pastEntries = ScheduleLogic.GetpastSchedules();

        foreach (ScheduleModel entry in pastEntries)
        {
            text += $"\n[{entry.Id}] room: {entry.Auditorium.Room}, movie: {entry.Movie.Name}, date: {entry.StartTime}";
            valid.Add((int)entry.Id);
        }

        ScheduleModel Selected = pastEntries.First(ScheduleModel => ScheduleModel.Id == General.ValidAnswer(text, valid));
        Console.WriteLine($"income: {Logic.CalculateIncome(Selected)} / {Logic.CalculateMaxIncome(Selected)}");
        Console.WriteLine($"Amount of empty seats: {Logic.EmptySeats(Selected)}");
    }


    public static void TotalMovieIncome()
    {
        Console.Clear();
        string text = "What movie do you want to show?";
        List<MovieModel> Movies = MovieLogic.GetAll();
        List<int> valid = [];
        foreach (MovieModel movie in Movies)
        {
            text += $"\n[{movie.Id}] {movie.Name}";
            valid.Add((int)movie.Id);
        }

        MovieModel Selected = Movies.First(MovieModel => MovieModel.Id == General.ValidAnswer(text, valid));
        List<ScheduleModel> entryWithMovie = ScheduleLogic.GetpastSchedulesWithMovie(Selected);
        List<Tuple<ScheduleModel, double>> scheduleEntries = Logic.CalculatePerSchedule(entryWithMovie);
        double cnt = 0;
        foreach (Tuple<ScheduleModel, double> entry in scheduleEntries)
        {
            Console.WriteLine($"Schedule: {entry.Item1.Id}, Income: {entry.Item2}, Empty seats {Logic.EmptySeats(entry.Item1)}");
            cnt += entry.Item2;
        }
        Console.WriteLine($"\nTotal income: {cnt}");
    }
}