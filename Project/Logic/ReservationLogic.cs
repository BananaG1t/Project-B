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
        int ColumnSize = AuditoriumLayoutAccess.GetColSizeByRoomId(Auditorium_ID);

        for (int i = 0; i < SeatClasses.Count(); i++)
        {
            for (int j = 1; j < ColumnSize; j++)
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
        int amount;
        do
        {
            Console.WriteLine("How many seats?");
            int.TryParse(Console.ReadLine(), out amount);
        } while (amount <= 0);

        ScheduleModel schedule = PickSchedule();
        int row = 0; int col = 1;
        int last_row;
        int last_col;
        int maxRow = schedule.Auditorium.Seats.Keys.Max(k => k.Row);
        int maxCol = schedule.Auditorium.Seats.Keys.Max(k => k.Collum);
        findNext(row, col, "down");
        if (row == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No row of seats available");
            Console.ResetColor();
            return;
        }
        ConsoleKey input = ConsoleKey.None;

        bool isValid(int row, int col)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!schedule.Auditorium.Seats.ContainsKey((row, col + i)) || !schedule.Auditorium.Seats[(row, col + i)].IsAvailable)
                    return false;
            }
            return true;
        }

        void findNext(int newRow, int newCol, string direction)
        {
            while (true)
            {
                if (direction == "down")
                    newRow += 1;
                else if (direction == "up")
                    newRow -= 1;

                if (newRow > maxRow || newRow < 1)
                    return;

                bool left = true;
                bool right = true;
                int cnt = 0;

                if (direction == "left")
                {
                    right = false;
                    cnt++;
                }
                else if (direction == "right")
                {
                    left = false;
                    cnt++;
                }

                while (true)
                {
                    if (newCol + (amount - 1) + cnt > maxCol)
                    {
                        right = false;
                    }
                    if (newCol - cnt < 1)
                    {
                        left = false;
                    }

                    if (!right && !left)
                        break;

                    if (right)
                        if (isValid(newRow, newCol + cnt))
                        {
                            row = newRow;
                            col = newCol + cnt;
                            return;
                        }
                    if (left)
                        if (isValid(newRow, newCol - cnt))
                        {
                            row = newRow;
                            col = newCol - cnt;
                            return;
                        }
                    cnt++;
                }
                if (direction == "left" || direction == "right")
                    return;
            }
        }

        do
        {
            last_row = row;
            last_col = col;
            if (input == ConsoleKey.DownArrow)
                if (isValid(row + 1, col))
                    row += 1;
                else findNext(row, col, "down");
            else if (input == ConsoleKey.UpArrow)
                if (isValid(row - 1, col))
                    row -= 1;
                else findNext(row, col, "up");
            else if (input == ConsoleKey.LeftArrow)
                if (isValid(row, col - 1))
                    col -= 1;
                else findNext(row, col, "left");
            else if (input == ConsoleKey.RightArrow)
                if (isValid(row, col + 1))
                    col += 1;
                else findNext(row, col, "right");

            AuditoriumLogic.DisplaySeats(schedule.Auditorium, row, col, amount);
            input = Console.ReadKey().Key;
            Console.Clear();
        } while (input != ConsoleKey.Enter);

        for (int i = 0; i < amount; i++)
        {
            SeatModel seat = schedule.Auditorium.Seats[(row, col + i)];
            seat.IsAvailable = false;
            SeatsAccess.Update(seat);
            ReservationAcces.Write(new(account.Id, (int)schedule.Id, seat.Row, seat.Collum));
        }
        Console.WriteLine("Made the reservation");

        string text = "would you like to buy snacks?\n[1] Yes\n[2] No";
        int choice = General.ValidAnswer(text, [1, 2]);
        if (choice == 1)
        {
            SnackReservation.BuySnacks(account);
        }



        //List<SeatModel> AllSeats = AssignSeats(MakeSeatList());

        //foreach (SeatModel seat in AllSeats)
        //{
        //ReservationAcces.Write(new(account.Id, (int)schedule.Id, seat.Row, seat.Collum, status));
        //}

        BarReservation.GetBarReservation(account, schedule, amount, 1);

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
        int answer = General.ValidAnswer(text, valid);
        return reservations.First(ReservationModel => ReservationModel.Id == answer);
    }

    public static Int64 GetReservation_id(Int64 id)
    {
        return ReservationAcces.GetReservation_id(id);
    }

}