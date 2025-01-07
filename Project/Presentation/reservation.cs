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
        ReservationModel reservation = SelectReservation(order);

        Console.Clear();
        string text =
        "What do you want to do?\n" +
        "[1] Cancel\n" +
        "[2] Back\n";

        int choice = PresentationHelper.MenuLoop(text, [1, 2]);

        switch (choice)
        {
            case 1:
                string confirmText =
                "Are you sure you want to cancel the reservation?\n" +
                "[1] Yes \n" +
                "[2] No\n";

                int confirmChoice = PresentationHelper.MenuLoop(confirmText, [1, 2]);

                if (confirmChoice == 1)
                {
                    if (reservation.Status == "Canceled")
                    {
                        PresentationHelper.Error("Already canceled");
                        return;
                    }

                    reservation.Status = "Canceled";
                    SeatModel seat = SeatLogic.GetByReservationInfo(
                        reservation.Seat_Collum,
                        reservation.Seat_Row,
                        ScheduleAccess.GetById(order.ScheduleId).AuditoriumId

                    );
                    seat.IsAvailable = true;
                    order.Amount--;

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
            text += $"\n[{i + 1}] Seat: row {reservations[i].Seat_Row} collum {reservations[i].Seat_Collum}, Status: {reservations[i].Status}";
        }

        int answer = PresentationHelper.MenuLoop(text, 1, reservations.Count);
        return reservations[answer - 1];
    }
}