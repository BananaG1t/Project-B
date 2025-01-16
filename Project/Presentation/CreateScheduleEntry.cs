using System.Globalization;

public static class CreateScheduleEntry
{
    // this for only to ignore the exception caused by Console.Clear() in the test environment
    public static bool IsTesting { get; set; } = false;

    public static void Main(AccountModel account)
    {
        LocationModel? location = LocationMenu.SelectLocation(account, canAdd: true, addSchedule: true);
        if (location is null) return;
        int room = SelectRoom();
        MovieModel movie = SelectMovie(canAdd : true);
        if (movie != null)
        {
            DateTime date = SelectDate(room, movie.Length, (int)location.Id);
            string? extras = GetExtras();
            new ScheduleModel(date, movie, new AuditoriumModel(room, extras), location);
            Console.Clear();
        }
        
        else
        {
            Console.WriteLine("\nSchedule creation cancelled\n");
        }
    }

    private static int SelectRoom()
    {
        Console.Clear();
        string text =
        "What room do you want to manage?\n" +
        "[1] Auditorium 1, size 150\n" +
        "[2] Auditorium 2, size 300\n" +
        "[3] Auditorium 3, size 500";

        return PresentationHelper.MenuLoop(text, 1, 3);
    }

    private static MovieModel? SelectMovie(bool canAdd = false)
    {
        Console.Clear();
        string text = "What movie do you want to show?";
        List<MovieModel> Movies = MovieLogic.GetAll();
        if (Movies.Count == 0)
        {
            PresentationHelper.Error("No movies found");
            if (!canAdd) return null;

            string confirmText =
                    "There are no movies\n" +
                    "Do you want to add a new movie?\n" +
                    "[1] Yes \n" +
                    "[2] No\n";

            int confirmChoice = PresentationHelper.MenuLoop(confirmText, 1, 2);
            if (confirmChoice == 1)
            {
                AddMovieMenu.Main();
                Console.WriteLine();
                Movies = MovieLogic.GetAll();
            }
            else if (confirmChoice == 2) return null;
        }

        for (int i = 0; i < Movies.Count; i++)
            {
                text += $"\n[{i + 1}] {Movies[i].Name}";
            }

        int answer = PresentationHelper.MenuLoop(text, 1, Movies.Count);
        return Movies[answer - 1];
    }

    public static DateTime SelectDate(int room, TimeSpan length, int locationId)
    {
        string text = "When do you want to show the movie? (dd-MM-yyyy-HH-mm)";
        DateTime date;
        date = PresentationHelper.ValidDate(text, format: "dd-MM-yyyy-HH-mm");

        if (!IsTesting)
        {
            Console.Clear();
        }
        if (!ScheduleLogic.IsAvailable(room, date, length, locationId))
        {
            Console.WriteLine("There is already a movie playing on that time");
            return SelectDate(room, length, locationId);
        }

        if (!ScheduleLogic.IsAvailable(room, date.AddMinutes(-20), length.Add(new TimeSpan(0, 20, 0)), locationId))
        {
            Console.WriteLine("Not enough time to clean the room");
            return SelectDate(room, length, locationId);
        }


        return date;
    }

    private static string? GetExtras()
    {
        Console.Clear();
        Console.WriteLine("Does it have any extras like IMAX? (leave blank if none)");
        string? Input = Console.ReadLine();
        return Input == "" ? null : Input;
    }

}