using System.ComponentModel.DataAnnotations.Schema;

class ReservationLogic
{
    public ReservationLogic()
    {

    }

    public static string GetDateAndTime()
    {
        List<string> AvailibleDays = DisplayDatesAndTimes();
        if (AvailibleDays == null) { return null; }
        string text = string.Join("\n", AvailibleDays);

        int input = General.ValidAnswer(text, General.ListMaker(1, AvailibleDays.Count() + 1));

        string ChosenDayAndTime = AvailibleDays[input - 1];
        ChosenDayAndTime = ChosenDayAndTime.Remove(0, 4);

        return ChosenDayAndTime;
    }

    public static List<string> DisplayDatesAndTimes()
    {
        List<string> DateInfo = [];
        int i = 1;

        List<string> InfoList = [];

        // ask the database for all the start times in the schedule
        // List<string> InfoList = database shit

        if (InfoList.Count() == 0) { return null; }

        foreach (string info in InfoList) { DateInfo.Add($"[{i}] {info}"); i++; }

        return DateInfo;
    }

    public static int PickSeats()
    {
        return 1;
    }

    public static MovieModel PickMovie()
    {
        // Define movie options using your custom MovieModel class
        var movies = new MovieModel[]
        {
            new MovieModel("Shrek", "DreamWorks", "An ogre's journey with a donkey to rescue a princess.", new TimeSpan(1, 29, 0), "Comedy/Fantasy", 7, 8.1),
            new MovieModel("Blair Witch Project", "Haxan Films", "Found footage of a group lost in the woods.", new TimeSpan(1, 18, 0), "Horror/Mystery", 16, 6.5),
            new MovieModel("Pacific Rim", "Legendary Pictures", "Robots fight giant monsters to save the Earth.", new TimeSpan(2, 11, 0), "Action/Sci-Fi", 12, 7.0),
            new MovieModel("Cars", "Pixar", "A race car learns life lessons in a small town.", new TimeSpan(1, 57, 0), "Kids/Comedy", 6, 7.1),
            new MovieModel("Interstellar", "Paramount Pictures", "Astronauts venture through a wormhole to save humanity.", new TimeSpan(2, 49, 0), "Sci-Fi/Adventure", 10, 8.6)
        };

        string movieList = "[1] Movie 1\nShrek\n2001 ‧ Komedie/Fantasyfilm ‧ 1 u 29 m\n\n" +
                           "[2] Movie 2\nBlair Witch Project\n1999 ‧ Horror/Mysterie film ‧ 1 u 18 m\n\n" +
                           "[3] Movie 3\nPacific Rim\n2013 ‧ Actie/Sci-fi ‧ 2 u 11 m\n\n" +
                           "[4] Movie 4\nCars\n2006 ‧ Kinderen/Komedie ‧ 1 u 57 m\n\n" +
                           "[5] Movie 5\nInterstellar\n2014 ‧ Sci-fi/Avontuur ‧ 2 u 49 m\n";

        // Generate valid movie numbers using ListMaker (1 to 5)
        List<int> validInputs = General.ListMaker(1, 5);
        MovieModel selectedMovie = null;
        bool isValid = false;

        while (!isValid)
        {
            // Display the movie list
            Console.WriteLine(movieList);

            // Ask the user for a valid movie number
            int selectedMovieNumber = General.ValidAnswer(movieList, validInputs);

            // Get the selected movie based on the user's choice
            selectedMovie = movies[selectedMovieNumber - 1];

            // Ask for confirmation
            Console.WriteLine($"You selected Movie {selectedMovieNumber}: {selectedMovie.Name}. Are you sure? (yes/no): ");
            string confirmation = Console.ReadLine().ToLower();

            if (confirmation == "yes")
            {
                Console.WriteLine($"You confirmed Movie {selectedMovieNumber}: {selectedMovie.Name}.");
                isValid = true;  // Exit the loop
            }
            else if (confirmation == "no")
            {
                Console.WriteLine("Let's try again.\n");
            }
            else
            {
                Console.WriteLine("Invalid response. Returning to the movie list.\n");
            }
        }

        return selectedMovie;
    }

    public static double GetSeatPrice(int seatClass)
    {
        return AuditoriumLayoutAccess.GetPriceBySeatClass(seatClass);
    }

    public static List<SeatModel> AssignSeats(List<int> SeatClasses)
    {
        List<SeatModel> AllSeats = [];

        int Auditorium_ID = 1;
        int RowSize = AuditoriumLayoutAccess.GetRowSizeByRoomId(Auditorium_ID);
        int ColumSize = AuditoriumLayoutAccess.GetColSizeByRoomId(Auditorium_ID);

        for (int i = 0; i < SeatClasses.Count(); i++)
        {
            for (int j = 1; j < ColumSize; j++)
            {
                for (int k = 1; k < RowSize; k++)
                {
                    SeatModel seat = SeatsAccess.GetByReservationInfo(k, j, Auditorium_ID, i);
                    if (!seat.IsAvailable)
                    {
                        seat.IsAvailable = true;
                        SeatsAccess.Update(seat);
                        AllSeats.Add(seat);
                    }
                }
            }
        }
        return AllSeats;
    }

    public static List<int> MakeSeatList(int SeatAmount, bool SameClass = true)
    {
        List<int> SeatClassList = [];

        if (SameClass)
        {
            string text = "In which class do you want to sit?\n";
            text += "[1] Class 1 - €15\n[2] Class 2 - €12.50\n[3] Class 3 - €10";
            int SeatClass = General.ValidAnswer(text, [1, 2, 3]);
            for (int i = 0; i < SeatAmount; i++)
            {
                SeatClassList.Add(SeatClass);
            }
        }
        else
        {
            for (int i = 1; i < SeatAmount; i++)
            {
                string text = "In which class do you want to sit?\n";
                text += "[1] Class 1 - €15\n[2] Class 2 - €12.50\n[3] Class 3 - €10";
                int SeatClass = General.ValidAnswer(text, [1, 2, 3]);
                SeatClassList.Add(SeatClass);
            }
        }
        return SeatClassList;
    }

    public static ScheduleModel PickSchedule()
    {
        List<ScheduleModel> Schedules = ScheduleAccess.ScheduleByDate();

        int i = 1;

        string text = "";
        foreach (ScheduleModel schedule in Schedules)
        {
            text += $"[{i}] Movie: {schedule.Movie.Name}, Room: {schedule.Auditorium.Room}, Starting time: {schedule.StartTime.ToString("HH:mm:ss")}";
            i++;
        }

        int input = General.ValidAnswer(text, General.ListMaker(1, Schedules.Count()));

        return Schedules[input - 1];
    }

    public static void GetReservation(AccountModel account)
    {
        // pick seat amount
        // pick seat class
        // same class?
        ScheduleModel schedule = PickSchedule();
        string status = "Active";
        foreach (SeatModel seat in AllSeats)
        {
            ReservationAcces.Write(new(account.Id, schedule.Id, seat.Row, seat.Collum, status));
        }
    }
}