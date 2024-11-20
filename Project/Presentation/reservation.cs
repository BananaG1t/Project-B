static class Reservation
{
    public static void ManageReservations(AccountModel account)
    {
        ReservationModel reservation = ReservationLogic.SelectReservation(account);

        Console.Clear();
        string text =
        "What do you want to do?\n" +
        "[1] Cancel\n" +
        "[2] Back\n";


        int choice = General.ValidAnswer(text, [1, 2]);

        switch (choice)
        {
            case 1:
                string confirmText =
                "Are you sure you want to cancel the reservation?\n" +
                "[1] Yes \n" +
                "[2] No\n";

                int confirmChoice = General.ValidAnswer(confirmText, [1]);

                if (confirmChoice == 1)
                {
                    if (reservation.Status == "Canceled")
                    {
                        Console.WriteLine("Already canceled");
                        return;
                    }

                    reservation.Status = "Canceled";
                    SeatModel seat = SeatsAccess.GetByReservationInfo(
                        reservation.Seat_Collum,
                        reservation.Seat_Row,
                        ScheduleAccess.GetById((int)reservation.Schedule_ID).AuditoriumId
                    );
                    seat.IsAvailable = true;

                    ReservationAcces.Update(reservation);
                    SeatsAccess.Update(seat);

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
}