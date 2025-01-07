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

        for (int i = 0; i < pastEntries.Count; i++)
        {
            text += $"\n[{i + 1}] room: {pastEntries[i].Auditorium.Room}, movie: {pastEntries[i].Movie.Name}, date: {pastEntries[i].StartTime}";
        }

        int input = PresentationHelper.MenuLoop(text, 1, pastEntries.Count) - 1;

        ScheduleModel Selected = pastEntries[input];

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
                      "[3] Show the total income from snacks by movie\n";

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
                OrderModel? order = GetOrder();
                if (order is null)
                {
                    Console.WriteLine("There are no orders to select from");
                    return;
                }
                TotalSnackIncomePerReservation(order);
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

    public static void TotalSnackIncomePerReservation(OrderModel order)
    {
        SnacksLogic.CalculateIncomeByReservation(order);

    }

    public static OrderModel? GetOrder()
    {
        List<OrderModel> orders = OrderLogic.GetAllActive();
        if (orders.Count == 0) return null;

        string text = "";

        for (int i = 0; i < orders.Count; i++)
            text += $"[{i + 1}] Order id: {orders[i].Id}";

        return orders[PresentationHelper.MenuLoop(text, 1, orders.Count)];
    }



}