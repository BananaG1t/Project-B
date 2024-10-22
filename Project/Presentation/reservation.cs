static class Reservation
{
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
        }
        else { Menu.Main(CurrentAccount); }
    }

    public static void PickMovie(string DateAndTime)
    {
        // show the user all tbe movies and let them pick one
    }
}