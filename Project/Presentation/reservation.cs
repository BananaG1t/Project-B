static class Reservation
{
    public static void ManageReservations(OrderModel order)
    {
        ReservationModel reservation = ReservationLogic.SelectReservation(order);

        Console.Clear();
        string text =
        "What do you want to do?\n" +
        "[1] Cancel\n" +
        "[2] Back\n";

        int choice = PresentationHelper.MenuLoop(text, 1, 2);

        switch (choice)
        {
            case 1:
                string confirmText =
                "Are you sure you want to cancel the reservation?\n" +
                "[1] Yes \n" +
                "[2] No\n";

                int confirmChoice = PresentationHelper.MenuLoop(confirmText, 1, 2);

                if (confirmChoice == 1)
                {
                    if (reservation.Status == "Canceled")
                    {
                        Console.WriteLine("Already canceled");
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

}