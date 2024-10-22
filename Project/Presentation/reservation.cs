static class Reservation
{
    public static void Start(AccountModel CurrentAccount)
    {
        string text = "Press [1] to pick a day and time\nPress [2] to go back";
        int input = General.ValidAnswer(text, [1, 2]);

        if (input == 1)
        {
            ReservationLogic.Start();
        }
        else { Menu.Main(CurrentAccount); }
    }

    public static void PickMovie()
    {
        // show the user all tbe movies and let them pick one
    }

    public static void PickTime(string ChosenDay)
    {
        Console.WriteLine(ChosenDay);
    }


}