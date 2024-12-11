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

        Console.Clear();
        string text = "At which location do you want to see?";
        List<LocationModel> ScheduleLocations = ScheduleAccess.GetAllLocationsWithSchedules();
        List<LocationModel> NoScheduleLocations = LocationLogic.GetAllLocationsWithNoSchedules();
        Dictionary<bool, List<LocationModel>> AllLocations = new Dictionary<bool, List<LocationModel>>
        {
            { true, [] },
            { false, [] }
        };
        List<int> valid = [];

        // Adds all locations with schedules to dict and as a valid option for reserving
        foreach (LocationModel location in ScheduleLocations)
        {
            AllLocations[true].Add(location);
            valid.Add((int)location.Id);
        }

        // Adds all locations with no schedules to dict without adding it as a valid option for reserving
        foreach (LocationModel location in NoScheduleLocations)
        {
            AllLocations[false].Add(location);
        }


        foreach (var locations in AllLocations)
        {
            if (locations.Key)
            {
                for (int i = 0; i < locations.Value.Count; i++)
                {
                    text += $"\n[{i + 1}] {locations.Value[i].Name}";
                }
            }
            else
            {
                foreach (LocationModel location in locations.Value)
                {
                    text += $"\n{location.Name} (Coming Soon!)";
                }
            }
        }

        int LocationId = PresentationHelper.MenuLoop(text, valid);
        LocationModel Location = ScheduleLocations.First(LocationModel => LocationModel.Id == LocationId);

        List<ScheduleModel> Schedules = ScheduleAccess.ScheduleByDateAndLocation(Location);


        text = "";

        for (int i = 0; i < Schedules.Count; i++)
        {
            text += $"\n[{i + 1}] Movie: {Schedules[i].Movie.Name}, Room: {Schedules[i].Auditorium.Room}, Starting time: {Schedules[i].StartTime}";

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
        if (schedule is null) { return; }
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
            else if (input == ConsoleKey.Backspace)
                return;

            AuditoriumLogic.DisplaySeats(schedule.Auditorium, row, col, amount);
            input = Console.ReadKey().Key;
            Console.Clear();
        } while (input != ConsoleKey.Enter);


        bool bar;
        if (OrderLogic.CheckBarSeats(schedule, amount))
        {
            bar = PresentationHelper.MenuLoop("Do you want to stay at the bar after the movie?\n[1] yes\n[2] no", 1, 2) == 1 ? true : false;
        }
        else
        {
            Console.WriteLine("Sorry, the bar is already full");
            bar = false;
        }

        OrderModel order = new(account.Id, schedule.Id, amount, bar);
        int reservationId;

        bool snack = false;
        string text = "would you like to buy snacks?\n[1] Yes\n[2] No";
        snack = PresentationHelper.MenuLoop(text, 1, 2) == 1;

        for (int i = 0; i < amount; i++)
        {
            SeatModel seat = schedule.Auditorium.Seats[(row, col + i)];
            seat.IsAvailable = false;
            SeatsAccess.Update(seat);
            reservationId = ReservationAcces.Write(new(order.Id, seat.Row, seat.Collum));
            if (snack)
                SnackReservation.BuySnacks(reservationId);
        }
        Console.WriteLine("Made the reservation");
    }

    public static List<ReservationModel> GetFromOrder(OrderModel order)
    {
        return ReservationAcces.GetFromOrder(order);
    }

    public static ReservationModel SelectReservation(OrderModel order)
    {
        Console.Clear();
        ScheduleModel schedule = ScheduleLogic.GetById(order.ScheduleId);
        string text = $"Location: {schedule.Location.Name}, Movie: {schedule.Movie.Name}, Date: {schedule.StartTime} Bar: {order.Bar}\nWhat reseration do you want to manage?";
        List<ReservationModel> reservations = GetFromOrder(order);
        List<int> valid = [];

        foreach (ReservationModel reservation in reservations)
        {
            text += $"\n[{reservation.Id}] Seat: row {reservation.Seat_Row} collum {reservation.Seat_Collum}, Status: {reservation.Status}";
            valid.Add(reservation.Id);
        }

        int answer = PresentationHelper.MenuLoop(text, 1, reservations.Count);
        return reservations.First(ReservationModel => ReservationModel.Id == answer);
    }

    public static Int64 GetReservation_id(Int64 id)
    {
        return ReservationAcces.GetReservation_id(id);
    }

}