static class Overview
{
    public static void MoneyOverview()
    {
        Console.Clear();
        string text = "Welcome to the income overview\n" +
                      "[1] Show income from all movies\n" +
                      "[2] Show movie income from a specific schedule entry\n" +
                      "[3] Show income from a specific movie\n" +
                      "[4] Show the snack income";
        int input = PresentationHelper.MenuLoop(text, 1, 4);

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
            case 4:
                SnackIncome();
                break;
        }
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

        if (pastEntries.Count == 0)
        {
            PresentationHelper.Error("No past schedules found");
            return;
        }

        for (int i = 0; i < pastEntries.Count; i++)
        {
            text += $"\n[{i + 1}] room: {pastEntries[i].Auditorium.Room}, movie: {pastEntries[i].Movie.Name}, date: {pastEntries[i].StartTime}";
        }

        int input = PresentationHelper.MenuLoop(text, 1, pastEntries.Count);

        ScheduleModel Selected = pastEntries[input - 1];

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

    public static void SnackIncome()
    {
        Console.Clear();
        string text = "Welcome to the snack income overview\n" +
                      "[1] Show the weekly total income from snacks\n" +
                      "[2] Show the daily total income from snacks\n" +
                      "[3] Show the total income from snacks by movie\n" +
                      "[4] Show the total income from snacks per reservation";

        int input = PresentationHelper.MenuLoop(text, 1, 4);

        switch (input)
        {
            case 1:
                TotalWeeklySnackIncome();
                break;
            case 2:
                TotalDailySnackIncome();
                break;
            case 3:
                TotalMovieSnackIncome();
                break;
            case 4:
                BoughtSnacksModel? BoughtSnacks = GetBoughtSnacksModel();
                if (BoughtSnacks is null)
                {
                    Console.WriteLine("There are no orders to select from");
                    return;
                }
                TotalSnackIncomePerReservation(BoughtSnacks);
                break;


        }
    }

    public static void TotalWeeklySnackIncome()
    {
        Console.Clear();
        double weeklyIncome = SnacksLogic.CalculateWeeklyIncome();

        if (weeklyIncome == 0)
        {
            Console.WriteLine("No snack sales recorded for the past week.");
            return;
        }

        Console.WriteLine($"Total weekly snack income: {weeklyIncome:C}");
    }

    public static void TotalDailySnackIncome()
    {
        Console.Clear();
        double dailyIncome = SnacksLogic.CalculateDailyIncome();

        if (dailyIncome == 0)
        {
            Console.WriteLine("No snack sales recorded for today.");
            return;
        }

        Console.WriteLine($"Total daily snack income: {dailyIncome:C}");
    }

    public static void TotalMovieSnackIncome()
    {
        Console.Clear();
        string text = "Select a movie to view snack income:";

        List<MovieModel> movies = MovieLogic.GetAll();
        if (!movies.Any())
        {
            Console.WriteLine("No movies available.");
            return;
        }

        foreach (MovieModel movie in movies)
        {
            text += $"\n[{movie.Id}] {movie.Name}";
        }

        int input = PresentationHelper.MenuLoop(text, 1, movies.Count);

        MovieModel selectedMovie = movies.First(movie => movie.Id == input);
        double movieSnackIncome = SnacksLogic.CalculateIncomeByMovie(selectedMovie);

        Console.WriteLine($"Total snack income for movie '{selectedMovie.Name}': {movieSnackIncome:C}");
    }

    public static void TotalSnackIncomePerReservation(BoughtSnacksModel BoughtSnacks)
    {
        Console.WriteLine($"Total snack income for reservation: €{SnacksLogic.CalculateIncomeByReservation(BoughtSnacks):F2}");
    }

    public static ReservationModel? GetReservationModel()
    {
        List<ReservationModel> reservations = ReservationAcces.GetAllActive();
        if (reservations.Count == 0) return null;

        string text = "";

        for (int i = 0; i < reservations.Count; i++)
            text += $"[{i + 1}] Order id: {reservations[i].Id}\n";

        return reservations[PresentationHelper.MenuLoop(text, 1, reservations.Count) - 1];
    }

    public static BoughtSnacksModel? GetBoughtSnacksModel()
    {
        List<BoughtSnacksModel> boughtSnacks = BoughtSnacksLogic.GetAll();

        List<BoughtSnacksModel> uniqueBoughtSnacks = [];
        List<int> validIds = [];
        List<int> validChoices = [];

        if (boughtSnacks.Count == 0) return null;

        string text = "";

        for (int i = 0; i < boughtSnacks.Count; i++)
        {
            if (!validIds.Contains(boughtSnacks[i].ReservationId))
            {
                uniqueBoughtSnacks.Add(boughtSnacks[i]);
                validIds.Add(boughtSnacks[i].ReservationId);
                validChoices.Add(validIds.Count);
                text += $"[{validIds.Count}] Reservation id: {boughtSnacks[i].ReservationId}\n";
            }

        }
        return uniqueBoughtSnacks[PresentationHelper.MenuLoop(text, validChoices) - 1];
    }



}