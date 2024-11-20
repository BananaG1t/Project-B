class ReservationLogic
{
    public ReservationLogic()
    {

    }

    public static void Update(ReservationModel reservation)
    {
        ReservationAcces.Update(reservation);
    }

    public static ScheduleModel PickSchedule()
    {
        List<ScheduleModel> Schedules = ScheduleAccess.ScheduleByDate();

        string text = "";

        for (int i = 1; i < Schedules.Count + 1; i++)
        {
            text += $"\n[{i}] Movie: {Schedules[i].Movie.Name}, Room: {Schedules[i].Auditorium.Room}, Starting time: {Schedules[i].StartTime}";
        }

        int input = PresentationHelper.MenuLoop(text, 1, Schedules.Count + 1);

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

        foreach (ReservationModel reservation in reservations)
        {
            ScheduleModel schedule = ScheduleAccess.GetById((int)reservation.Schedule_ID);
            text += $"\n[{reservation.Id}] Movie: {schedule.Movie.Name}, Date: {schedule.StartTime}, Seat: row {reservation.Seat_Row} collum {reservation.Seat_Collum}, Status: {reservation.Status}";
        }

        int answer = PresentationHelper.MenuLoop(text, 1, reservations.Count);
        return reservations.First(ReservationModel => ReservationModel.Id == answer);
    }
}