using System.Globalization;

public static class CreateScheduleEntry
{
    public static void Main(AccountModel account)
    {
        LocationModel location = Location(account);
        int room = SelectRoom();
        MovieModel movie = SelectMovie();
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

    private static MovieModel? SelectMovie()
    {
        Console.Clear();
        string text = "What movie do you want to show?";
        List<MovieModel> Movies = MovieLogic.GetAll();
        if (Movies.Count == 0)
        {
            PresentationHelper.Error("No locations with schedule entries");

            string confirmText =
                    "There are no movies\n" +
                    "Do you want to add a new movie?\n" +
                    "[1] Yes \n" +
                    "[2] No\n";

            int confirmChoice = PresentationHelper.MenuLoop(confirmText, 1, 2);
            if (confirmChoice == 1)
            {
                AddMovieMenu.Main();
                Movies = MovieLogic.GetAll();
            }
            else if (confirmChoice == 2) return null;
        }
        List<int> valid = [];
        foreach (MovieModel movie in Movies)
        {
            text += $"\n[{movie.Id}] {movie.Name}";
            valid.Add((int)movie.Id);
        }

        int answer = PresentationHelper.MenuLoop(text, 1, Movies.Count);
        return Movies.First(MovieModel => MovieModel.Id == answer);
    }

    private static DateTime SelectDate(int room, TimeSpan length, int locationId)
    {
        string text = "When do you want to show the movie? (dd-MM-yyyy-HH-mm)";
        DateTime date;
        date = General.ValidDate(text);

        Console.Clear();
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

    private static LocationModel Location(AccountModel account)
    {
        Console.Clear();

        AssignedRoleModel assignedRole = RoleLogic.GetAssignedRoleByAccountId(account.Id);
        RoleModel role = RoleLogic.GetRoleById((int)assignedRole.RoleId);
        if (role.LevelAccess < 255)
        { return LocationLogic.GetById((int)assignedRole.LocationId); }

        string text = "At which location do you want to see?";
        List<LocationModel> locations = LocationLogic.GetAll();

        for (int i = 0; i < locations.Count; i++)
        {
            text += $"\n[{i + 1}] {locations[i].Name}";

        }
        int LocationId = PresentationHelper.MenuLoop(text, 1, locations.Count);
        LocationModel Location = locations[LocationId - 1];
        return Location;
    }

}