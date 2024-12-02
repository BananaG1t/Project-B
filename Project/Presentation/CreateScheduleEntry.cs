using System.Globalization;

public static class CreateScheduleEntry
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

        int movieId = General.ValidAnswer(text, valid);
        return Movies.First(MovieModel => MovieModel.Id == movieId);
    }

    private static DateTime SelectDate(int room, TimeSpan length)
    {
        Console.Clear();
        string text = "When do you want to show the movie? (dd-MM-yyyy-HH-mm)";
        DateTime date;
        date = General.ValidDate(text);
        bool cleanupTime = CleanupTime(date);
        while (!ScheduleLogic.IsAvailable(room, date, length) || !cleanupTime)
        {
            Console.Clear();
            if (!ScheduleLogic.IsAvailable(room, date, length))
            {
                Console.WriteLine("There is already a movie playing on that time");
            }
            else
            {
                Console.WriteLine("Not enough time to clean the room");
            }    
                 
            date = General.ValidDate(text);
            cleanupTime = CleanupTime(date);
        }

        return date;
    }

    public static bool CleanupTime(DateTime date)
    {
        bool enoughTime = true;
        List<ScheduleModel> Schedules = ScheduleAccess.ScheduleByDate();
        TimeSpan CleanupTime = new TimeSpan(0,20,0);
         
        foreach (ScheduleModel schedule in Schedules)
        {
            if ((date - schedule.EndTime ) <= CleanupTime && (date - schedule.EndTime ) > TimeSpan.Zero)
            {
                enoughTime = false;
            }
        }
        return enoughTime;
    }

    private static string? GetExtras()
    {
        Console.Clear();
        Console.WriteLine("Does it have any extras like IMAX? (leave blank if none)");
        string? Input = Console.ReadLine();
        return Input == "" ? null : Input;
    }
}