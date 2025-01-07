class ReservationLogic
{
    public ReservationLogic()
    {

    }

    public static void Update(ReservationModel reservation)
    {
        ReservationAcces.Update(reservation);
    }

    public static void GetReservation(AccountModel account)
    {
        // pick a location
        LocationModel location = LocationMenu.SelectLocation();

        // pick a schedule
        ScheduleModel schedule = Schedule.SelectSchedule(location);

        // pick seat amount
        int amount = Reservation.SelectSeatAmount();

        if (schedule is null) { return; }
        int row = 0; int col = 1;
        int last_row;
        int last_col;
        int maxRow = schedule.Auditorium.Seats.Keys.Max(k => k.Row);
        int maxCol = schedule.Auditorium.Seats.Keys.Max(k => k.Collum);
        findNext(row, col, "down");
        if (row == 0)
        {
            PresentationHelper.Error("No row of seats available");
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

            Seats.DisplaySeats(schedule.Auditorium, row, col, amount);
            input = Console.ReadKey().Key;
        } while (input != ConsoleKey.Enter);


        bool bar;
        if (OrderLogic.CheckBarSeats(schedule, amount))
        {
            bar = PresentationHelper.MenuLoop("Do you want to stay at the bar after the movie?\n[1] yes\n[2] no", 1, 2) == 1 ? true : false;
        }
        else
        {
            PresentationHelper.Error("Sorry, the bar is already full");
            bar = false;
        }

        CouponModel selectedCoupon = Coupon.SelectCoupon(account.Id);

        OrderModel order = new(account.Id, schedule.Id, amount, bar, selectedCoupon.Id);
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
                SnackReservation.BuySnacks(reservationId, i + 1);
        }

        List <ReservationModel> reservations = GetFromOrder(order);
        double totalSeatPrice = 0;
        foreach(ReservationModel reservation in reservations)
        {
            SeatModel seat = SeatLogic.GetByReservationInfo(reservation.Seat_Collum, reservation.Seat_Row,schedule.AuditoriumId);
            seat.Price += totalSeatPrice;
        }
        
        if(selectedCoupon.CouponType == "Seats")
        {
            Coupon.discountprice(totalSeatPrice, selectedCoupon);
        }
        Console.WriteLine("Made the reservation");
    }

    public static List<ReservationModel> GetFromOrder(OrderModel order)
    {
        return ReservationAcces.GetFromOrder(order);
    }

    public static Int64 GetReservation_id(Int64 id)
    {
        return ReservationAcces.GetReservation_id(id);
    }

}