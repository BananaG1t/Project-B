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

    public static int PickSeats(ScheduleModel schedule)
    {
        return 1;
    }

    public static MovieModel PickMovie()
    {
        MovieModel selectedMovie = null;
        bool isValid = false;

        while (!isValid)
        {
            //Select a movie
            selectedMovie = SelectMovie();

            // Ask for confirmation
            Console.WriteLine($"You selected Movie {selectedMovie.Id}: {selectedMovie.Name}. Are you sure? (yes/no): ");
            string confirmation = Console.ReadLine().ToLower();

            if (confirmation == "yes")
            {
                Console.WriteLine($"You confirmed Movie {selectedMovie.Id}: {selectedMovie.Name}.");
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

    private static MovieModel SelectMovie()
    {
        Console.Clear();
        string text = "What movie do you want to see?";
        List<MovieModel> Movies = MovieLogic.GetAll();
        List<int> valid = [];
        foreach (MovieModel movie in Movies)
        {
            text += $"\n[{movie.Id}] {movie.Name}";
            valid.Add((int)movie.Id);
        }

        return Movies.First(MovieModel => MovieModel.Id == General.ValidAnswer(text, valid));
    }

    public static double GetSeatPrice(int seatClass)
    {
        return AuditoriumLayoutAccess.GetPriceBySeatClass(seatClass);
    }

    /*
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
    */

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
            text += $"\n[{i}] Movie: {schedule.Movie.Name}, Room: {schedule.Auditorium.Room}, Starting time: {schedule.StartTime}";
            i++;
        }

        int input = General.ValidAnswer(text, General.ListMaker(1, Schedules.Count + 1));

        return Schedules[input - 1];
    }

    public static void GetReservation(AccountModel account)
    {
        // pick seat amount
        // pick seat class
        // same class?
        ScheduleModel schedule = PickSchedule();
        AuditoriumLogic.DisplaySeats(schedule.Auditorium);
        int row;
        int collum;
        Console.WriteLine("What row?");
        int.TryParse(Console.ReadLine(), out row);
        Console.WriteLine("What collum?");
        int.TryParse(Console.ReadLine(), out collum);


        if (schedule.Auditorium.Seats.ContainsKey((schedule.Auditorium.Id, row, collum)))
        {
            SeatModel seat = schedule.Auditorium.Seats[(schedule.Auditorium.Id, row, collum)];
            seat.IsAvailable = false;
            SeatsAccess.Update(seat);
            ReservationAcces.Write(new(account.Id, (int)schedule.Id, seat.Row, seat.Collum));
            Console.WriteLine("Made the reservation");
        }

        else Console.WriteLine("Invalid");

        //List<SeatModel> AllSeats = AssignSeats(MakeSeatList());

        //foreach (SeatModel seat in AllSeats)
        //{
        //ReservationAcces.Write(new(account.Id, (int)schedule.Id, seat.Row, seat.Collum, status));
        //}
    }

    public static List<ReservationModel> GetFromAccount(AccountModel account)
    {
        return ReservationAcces.GetFromAccount(account);
    }

    public static ReservationModel SelectReservation(AccountModel account)
    {
        Console.Clear();
        string text = "What reseration do you want to manage?";
        List<ReservationModel> reservations = ReservationLogic.GetFromAccount(account);
        List<int> valid = [];

        foreach (ReservationModel reservation in reservations)
        {
            ScheduleModel schedule = ScheduleAccess.GetById((int)reservation.Schedule_ID);
            text += $"\n[{reservation.Id}] Movie: {schedule.Movie.Name}, Date: {schedule.StartTime}, Seat: row {reservation.Seat_Row} collum {reservation.Seat_Collum}, Status: {reservation.Status}";
            valid.Add(reservation.Id);
        }

        return reservations.First(ReservationModel => ReservationModel.Id == General.ValidAnswer(text, valid));
    }
}