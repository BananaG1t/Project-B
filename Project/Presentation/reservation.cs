static class Reservation
{

    public static void AdminMenu()
    {
        string text =
            "Manage reservations menu\n" +
            "Press [1] search by account id\n" +
            "Press [2] search by email\n" +
            "Press [3] search by order id\n" +
            "Press [4] to go back";



        while (true)
        {
            // get a valid input number
            int input = PresentationHelper.MenuLoop(text, 1, 4);
            if (input == 1)
            {
                int accountId = PresentationHelper.GetInt("Enter account id: ");
                AccountModel? account = AccountsLogic.GetById(accountId);
                if (account == null)
                {
                    PresentationHelper.Error("Account not found");
                    continue;
                }

                ManageReservations(OrderLogic.SelectOrder(account));
            }
            else if (input == 2)
            {
                Console.WriteLine("Enter email: ");
                string? email = Console.ReadLine();
                if (email == null)
                {
                    PresentationHelper.Error("Invalid email");
                    continue;
                }

                AccountModel? account = AccountsLogic.GetByEmail(email);
                if (account == null)
                {
                    PresentationHelper.Error("Account not found");
                    continue;
                }

                ManageReservations(OrderLogic.SelectOrder(account));
            }
            else if (input == 3)
            {
                int orderId = PresentationHelper.GetInt("Enter order id: ");
                OrderModel? order = OrderLogic.GetById(orderId);
                if (order == null)
                {
                    PresentationHelper.Error("Order not found");
                    continue;
                }

                ManageReservations(order);
            }
            else if (input == 4)
            {
                return;
            }
        }
    }

    public static void ManageReservations(OrderModel order)
    {
        string menu =
        "What do you want to manage?\n" +
        "[1] Whole order\n" +
        "[2] Individual reservations\n" +
        "[3] Manage bar\n" +
        "[4] Go back\n";

        while (true)
        {
            int menuSelected = PresentationHelper.MenuLoop(menu, 1, 4);

            switch (menuSelected)
            {
                case 1:
                    ManageWholeOrder(order);
                    break;
                case 2:
                    ManageIndividualReservations(order);
                    break;
                case 3:
                    ManageBar(order);
                    break;
                case 4:
                    return;
            }
        }
    }

    public static void ManageWholeOrder(OrderModel order)
    {
        Console.Clear();
        string text =
        "What do you want to do?\n" +
        "[1] Cancel\n" +
        "[2] Back";

        int choice = PresentationHelper.MenuLoop(text, 1, 3);


        List<ReservationModel> reservations = ReservationLogic.GetFromOrder(order);
        switch (choice)
        {
            case 1:
                string confirmText =
                "Are you sure you want to cancel the order?\n" +
                "[1] Yes \n" +
                "[2] No\n";

                int confirmChoice = PresentationHelper.MenuLoop(confirmText, [1, 2]);

                if (confirmChoice == 1)
                {
                    foreach (ReservationModel reservation in reservations)
                    {
                        if (reservation.Status == "Canceled")
                        {
                            continue;
                        }

                        reservation.Status = "Canceled";
                        SeatModel seat = SeatLogic.GetByReservationInfo(
                            reservation.Seat_Collum,
                            reservation.Seat_Row,
                            ScheduleAccess.GetById(order.ScheduleId).AuditoriumId

                        );
                        seat.IsAvailable = true;
                        List<BoughtSnacksModel> snacks = BoughtSnacksLogic.GetFromReservation(reservation);
                        foreach (BoughtSnacksModel snack in snacks)
                        {
                            BoughtSnacksLogic.Delete(snack.Id);
                        }

                        ReservationLogic.Update(reservation);
                        SeatLogic.Update(seat);
                    }

                    order.Amount = 0;
                    order.Bar = false;
                    OrderAccess.Update(order);

                    Console.WriteLine("The order has been canceled.");
                }
                else
                {
                    Console.WriteLine("Cancellation aborted.");
                }
                return;

            case 2:
                return;
        }
    }

    public static void ManageIndividualReservations(OrderModel order)
    {
        ReservationModel reservation = SelectReservation(order);

        Console.Clear();
        string text =
        "What do you want to do?\n" +
        "[1] Cancel\n" +
        "[2] Manage snacks\n" +
        "[3] Back";

        int choice = PresentationHelper.MenuLoop(text, 1, 3);

        switch (choice)
        {
            case 1:
                if (reservation.Status == "Canceled")
                {
                    PresentationHelper.Error("Already canceled");
                    return;
                }

                string confirmText =
                "Are you sure you want to cancel the reservation?\n" +
                "[1] Yes \n" +
                "[2] No";

                int confirmChoice = PresentationHelper.MenuLoop(confirmText, [1, 2]);

                if (confirmChoice == 1)
                {
                    reservation.Status = "Canceled";
                    SeatModel seat = SeatLogic.GetByReservationInfo(
                        reservation.Seat_Collum,
                        reservation.Seat_Row,
                        ScheduleAccess.GetById(order.ScheduleId).AuditoriumId

                    );
                    seat.IsAvailable = true;
                    order.Amount--;
                    List<BoughtSnacksModel> snacks = BoughtSnacksLogic.GetFromReservation(reservation);
                    foreach (BoughtSnacksModel snack in snacks)
                    {
                        BoughtSnacksLogic.Delete(snack.Id);
                    }

                    OrderAccess.Update(order);
                    ReservationLogic.Update(reservation);
                    SeatLogic.Update(seat);

                    Console.WriteLine("The reservation has been canceled.");
                }
                else
                {
                    Console.WriteLine("Cancellation aborted.");
                }
                return;

            case 2:
                if (reservation.Status == "Canceled")
                {
                    PresentationHelper.Error("Cant chage snacks on canceled reservation");
                    return;
                }

                List<BoughtSnacksModel> boughtSnacks = BoughtSnacksLogic.GetFromReservation(reservation);
                string snackSelectText = "Select a snack to manage:\n[0] new snack";
                for (int i = 0; i < boughtSnacks.Count; i++)
                {
                    SnacksModel snack = SnacksLogic.GetById(boughtSnacks[i].SnackId);
                    snackSelectText += $"\n[{i + 1}] Name: {snack.Name}, Price: {snack.Price:F2}, amount: {boughtSnacks[i].Amount}, total price: {snack.Price * boughtSnacks[i].Amount:F2}";
                }
                snackSelectText += $"\n[{boughtSnacks.Count + 1}] Back";
                int selectedSnack = PresentationHelper.MenuLoop(snackSelectText, 0, boughtSnacks.Count + 1);

                if (selectedSnack == boughtSnacks.Count + 1)
                {
                    return;
                }

                else if (selectedSnack == 0)
                {
                    SnackReservation.BuySnacks(reservation.Id, 1);
                }

                else
                {
                    SnacksModel snack = SnacksLogic.GetById(boughtSnacks[selectedSnack - 1].SnackId);
                    string snackManageText = $"What do you want to do with {snack.Name}?\n" +
                    "[1] Change amount\n" +
                    "[2] Remove\n" +
                    "[3] Back";

                    int snackManageChoice = PresentationHelper.MenuLoop(snackManageText, 1, 3);

                    switch (snackManageChoice)
                    {
                        case 1:
                            int newAmount = PresentationHelper.GetInt("Enter new amount: ");
                            boughtSnacks[selectedSnack - 1].Amount = newAmount;
                            BoughtSnacksLogic.Update(boughtSnacks[selectedSnack - 1]);
                            Console.WriteLine("Amount changed");
                            return;

                        case 2:
                            BoughtSnacksLogic.Delete(boughtSnacks[selectedSnack - 1].Id);
                            Console.WriteLine("Snack removed");
                            return;

                        case 3:
                            return;
                    }
                }

                return;

            case 3:
                return;
        }
    }

    public static void ManageBar(OrderModel order)
    {
        Console.Clear();
        string text =
        "What do you want to do?\n" +
        "[1] Add bar\n" +
        "[2] Remove bar\n" +
        "[3] Back\n";

        int choice = PresentationHelper.MenuLoop(text, 1, 3);

        switch (choice)
        {
            case 1:
                if (order.Amount == 0)
                {
                    PresentationHelper.Error("No seats booked");
                    return;
                }
                if (order.Bar)
                {
                    PresentationHelper.Error("Bar already added");
                    return;
                }

                if (!OrderLogic.CheckBarSeats(ScheduleLogic.GetById(order.ScheduleId), order.Amount))
                {
                    PresentationHelper.Error("Sorry, the bar is already full");
                    return;
                }

                order.Bar = true;
                OrderAccess.Update(order);
                Console.WriteLine("Bar added to the order.");
                return;

            case 2:
                if (!order.Bar)
                {
                    PresentationHelper.Error("Bar already removed");
                    return;
                }

                order.Bar = false;
                OrderAccess.Update(order);
                Console.WriteLine("Bar removed from the order.");
                return;

            case 3:
                return;
        }
    }

    public static int SelectSeatAmount()
    {
        while (true)
        {
            Console.WriteLine("How many seats do you want to book?");
            if (!int.TryParse(Console.ReadLine(), out int amount))
            {
                PresentationHelper.Error("Invalid input. Please enter a valid number.");
                continue; // Retry
            }
            if (amount <= 0)
            {
                PresentationHelper.Error("Number must be more than 0. Try again.");
                continue; // Retry
            }

            return amount; // Valid input
        }
    }

    public static ReservationModel SelectReservation(OrderModel order)
    {
        Console.Clear();
        ScheduleModel schedule = ScheduleLogic.GetById(order.ScheduleId);
        string text = $"Location: {schedule.Location.Name}, Movie: {schedule.Movie.Name}, Date: {schedule.StartTime} Bar: {order.Bar}\nWhat reservation do you want to manage?";
        List<ReservationModel> reservations = ReservationLogic.GetFromOrder(order);

        for (int i = 0; i < reservations.Count; i++)
        {
            text += $"\n[{i + 1}] Seat: row {reservations[i].Seat_Row} collum {reservations[i].Seat_Collum}, Status: {reservations[i].Status}, Snacks: {BoughtSnacksLogic.GetFromReservation(reservations[i]).Sum(x => x.Amount)}";
        }

        int answer = PresentationHelper.MenuLoop(text, 1, reservations.Count);
        return reservations[answer - 1];
    }
}