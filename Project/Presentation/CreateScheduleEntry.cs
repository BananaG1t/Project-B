using System.Globalization;

static class CreateScheduleEntry
{
    public static void Main()
    {
        int room = SelectRoom();
        MovieModel movie = SelectMovie();
        DateTime date = SelectDate(room, movie.Length);
        string? extras = GetExtras();
        new ScheduleModel(date, movie, new AuditoriumModel(room, extras));
        Console.Clear();
    }

    private static int SelectRoom()
    {
        Console.Clear();
        string text =
        "What room do you want to manage?\n" +
        "[1] Auditorium 1, size 150\n" +
        "[2] Auditorium 2, size 300\n" +
        "[3] Auditorium 3, size 500";

        return General.ValidAnswer(text, [1, 2, 3]);
    }

    private static MovieModel SelectMovie()
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

        return Movies.First(MovieModel => MovieModel.Id == General.ValidAnswer(text, valid));
    }

    private static DateTime SelectDate(int room, TimeSpan length)
    {
        Console.Clear();
        string text = "When do you want to show the movie? (dd-mm-yyy-hh-mm)";
        DateTime date;
        date = General.ValidDate(text);
        while (!ScheduleLogic.IsAvailable(room, date, length))
        {
            Console.Clear();
            Console.WriteLine("Date is unavailable");
            date = General.ValidDate(text);

        }
        return date;
    }

    private static string? GetExtras()
    {
        Console.Clear();
        Console.WriteLine("Does it have any extraslike IMAX? (leave blank if none)");
        string? Input = Console.ReadLine();
        return Input == "" ? null : Input;
    }
}