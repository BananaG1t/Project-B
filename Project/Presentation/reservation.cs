static class Reservation
{
    /*
    public static void Start(AccountModel CurrentAccount)
    {
        string text = "Press [1] to pick a day and time\nPress [2] to go back";
        int input = General.ValidAnswer(text, [1, 2]);

        if (input == 1)
        {
            string DateAndTime = ReservationLogic.GetDateAndTime();
            if (DateAndTime == null)
            {
                Console.WriteLine("There are no movies playing.\nYou will be sent back to the menu\n");
                Menu.Main(CurrentAccount);
            }
            else
            {
                List<int> SeatList = [];

                text = "How many seats would you like?";
                int SeatAmount = General.ValidAnswer(text, General.ListMaker(1, 100));

                if (SeatAmount > 1)
                {
                    text = "Do you want all the seats in the same seat class\n";
                    text += "[1] to get the seats in the same class\n";
                    text += "[2] to get seats in different classes\n";

                    if (General.ValidAnswer(text, [1, 2]) == 1) { SeatList = ReservationLogic.MakeSeatList(SeatAmount); }
                    else { SeatList = ReservationLogic.MakeSeatList(SeatAmount, false); }
                }
                else { SeatList = ReservationLogic.MakeSeatList(SeatAmount); }

                List<SeatModel> AllSeats = ReservationLogic.AssignSeats(SeatList);
            }
        }
        else { Menu.Main(CurrentAccount); }
    }
    */
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