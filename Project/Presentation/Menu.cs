static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    public static List<string> functionalities = ["Manage Users", "Add a movie", "Add to the schedule",
                                                    "Display the schedule", "Display income overview",
                                                    "Manage snacks", "Manage Locations", "Create coupon"];
    static public void AdminMenu(AccountModel account)
    {
        //admin menu
        List<string> usedFunctionalities = RoleLogic.GetMenuOptions(account);

        string text = $"Staff menu: {RoleLogic.GetRoleByAccountId(account.Id).Name}";

        for (int i = 0; i < usedFunctionalities.Count; i++)
        {
            text += $"\n[{i + 1}] {usedFunctionalities[i]}";
        }

        while (true)
        {
            //reading input from the menu to connect to the features
            string functionality = usedFunctionalities[PresentationHelper.MenuLoop(text, 1, usedFunctionalities.Count) - 1];

            if (functionality == functionalities[0])
            {
                Roles.RoleMenu();
            }
            else if (functionality == functionalities[1])
            {
                AddMovieMenu.Main();
            }
            else if (functionality == functionalities[2])
            {
                CreateScheduleEntry.Main(account);
            }
            else if (functionality == functionalities[3])
            {
                LocationModel? location = LocationMenu.SelectLocation(account, canAdd : true);
                if (location is not null) 
                    Schedule.DisplaySchedule(location);
            }
            else if (functionality == functionalities[4])
            {
                Overview.MoneyOverview();
            }
            else if (functionality == functionalities[5])
            {
                Console.Clear();
                SnackReservation.Main();
            }
            else if (functionality == functionalities[6])
            {
                LocationMenu.Main();
            }
            else if (functionality == functionalities[7])
            {
                Coupon.AdminMenu();
            }
            else if (functionality == "Exit")
            {
                Console.WriteLine("Exiting");
                break;
            }
        }

        Start();
    }

    static public void Start()
    {
        Console.Clear();
        // get a valid input number
        string text = $"Enter [1] to login\nEnter [2] to create new account";
        int input = PresentationHelper.MenuLoop(text, 1, 2);

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
            int input = PresentationHelper.MenuLoop(text, 1, 4);

            if (input == 1)
            {
                LocationModel? location = LocationMenu.SelectLocation(CurrentAccount);
                if (location is not null) 
                    ReservationLogic.GetReservation(CurrentAccount, location);
            }

            else if (input == 2)
            {
                Reservation.ManageReservations(Order.SelectOrder(CurrentAccount));
            }
            else if (input == 3)
            {
                LocationModel? location = LocationMenu.SelectLocation(CurrentAccount);
                if (location is not null) 
                    Schedule.DisplaySchedule(location);
            }
            // sends the user to the start to login again
            else if (input == 4) { break; }
        }

        Start();
    }


}