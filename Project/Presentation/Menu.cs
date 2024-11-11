static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void AdminMenu()
    {
        //admin menu
        string text =
        "Admin Menu:\n" +
        "[1] Manage Users (empty for now)\n" +
        "[2] Add a movie\n" +
        "[3] Add to the schedule\n" +
        "[4] Display the schedule\n" +
        "[5] Display income overview\n" +
        "[6] Exit";

        while (true)
        {
            //reading input from the menu to connect to the features
            int input = General.ValidAnswer(text, [1, 2, 3, 4, 5, 6]);

            if (input == 1)
            {
                Console.WriteLine("This feature is not yet implemented");
            }
            else if (input == 2)
            {
                AddMovieMenu.Main();
            }
            else if (input == 3)
            {
                CreateScheduleEntry.Main();
            }
            else if (input == 4)
            {
                DisplaySchedule();
            }
            else if (input == 5)
            {
                Overview.MoneyOverview();
            }
            else if (input == 6)
            {
                Console.WriteLine("Exiting");
                break;
            }
        }

        UserLogin.Start();
    }

    static public void Start()
    {
        Console.Clear();
        // get a valid input number
        int input = General.ValidAnswer("Enter [1] to login\nEnter [2] to create new account", [1, 2]);

        if (input == 1) { UserLogin.Start(); }
        else if (input == 2) { UserLogin.CreateLogin(); }
    }

    public static void Main(AccountModel CurrentAccount)
    {
        while (true)
        {
            string text =
            "User menu\n" +
            "Press [1] to make a new reservation\n" +
            "Press [2] to manage the reservations you have made\n" +
            "Press [3] to see movie schedules\n" +
            "Press [4] to log out";

            // get a valid input number
            int input = General.ValidAnswer(text, [1, 2, 3, 4]);

            if (input == 1)
            {
                ReservationLogic.GetReservation(CurrentAccount);
            }

            else if (input == 2)
            {
                Reservation.ManageReservations(CurrentAccount);
            }
            else if (input == 3)
            {
                DisplaySchedule();
            }
            // sends the user to the start to login again
            else if (input == 4) { break; }
        }

        UserLogin.Start();
    }

    public static void DisplaySchedule()
    {
        List<ScheduleModel> Schedules = ScheduleAccess.ScheduleByDate();

        // Shows what movie are playing based on the date and time
        Console.Clear();
        Console.WriteLine($"Movies Playing");
        foreach (ScheduleModel schedule in Schedules)
        {
            Console.WriteLine($"Movie: {schedule.Movie.Name}, Room: {schedule.Auditorium.Room}, Starting time: {schedule.StartTime}");
        }
        Console.WriteLine();
    }
}