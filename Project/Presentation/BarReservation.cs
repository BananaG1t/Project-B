static class BarReservation
{
    public static void GetBarReservation(AccountModel Account, ScheduleModel schedule, int SeatAmount, int reservationId)
    {
        string text = $"Do you want to sit at the bar after the movie?\n[1] Yes\n[2] No";

        bool choice = General.ValidAnswer(text, [1, 2]) == 1;
        if (!choice) { Console.Clear(); return; }
        if (BarReservationLogic.ReserveBarSeats(Account, schedule, SeatAmount, reservationId))
        {
            // Console.Clear();
            Console.WriteLine($"You have booked {SeatAmount} Seats for {schedule.EndTime}");
            return;
        }
        ;
    }
}