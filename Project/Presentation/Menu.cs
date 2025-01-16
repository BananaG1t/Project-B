static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    public static List<string> functionalities = ["Manage Users", "Add a movie", "Add to the schedule",
                                                    "Display the schedule", "Manage reservations", "Display income overview",
                                                    "Manage snacks", "Manage Locations", "Manage Coupons"];
    static public void AdminMenu(AccountModel account)
    {
        //admin menu
        List<string> usedFunctionalities = RoleLogic.GetMenuOptions(account);

        string text = $"Staff menu: {RoleLogic.GetRoleByAccountId(account.Id)?.Name}";

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
                AddMovieMenu.AddMovie();
            }
            else if (functionality == functionalities[2])
            {
                CreateScheduleEntry.AddSchedule(account);
            }
            else if (functionality == functionalities[3])
            {
                LocationModel? location = LocationMenu.SelectLocation(account, canAdd: true);
                if (location is not null)
                    Schedule.DisplaySchedule(location);
            }
            else if (functionality == functionalities[4])
            {
                Reservation.AdminMenu();
            }
            else if (functionality == functionalities[5])
            {
                Overview.MoneyOverview();
            }
            else if (functionality == functionalities[6])
            {
                Console.Clear();
                SnackReservation.SnackMenu();
            }
            else if (functionality == functionalities[7])
            {
                LocationMenu.MainMenu();
            }
            else if (functionality == functionalities[8])
            {
                Coupon.AdminMenu();
            }
            else if (functionality == "Logout")
            {
                Console.WriteLine("Logging out");
                break;
            }
        }

        Start();
    }

    static public void Start()
    {
        //Console.Clear();
        // get a valid input number
        string text = $"Enter [1] to login\nEnter [2] to create new account";
        int input = PresentationHelper.MenuLoop(text, 1, 2);

        if (input == 1) { UserLogin.Start(); }
        else if (input == 2) { UserLogin.CreateLogin(); }
    }

    public static void MainMenu(AccountModel CurrentAccount)
    {
        while (true)
        {
            string text =
            "User menu\n" +
            "Press [1] to make a new reservation\n" +
            "Press [2] to manage the reservations you have made\n" +
            "Press [3] to see movie schedules\n" +
            "Press [4] to display coupons\n" +
            "Press [5] to log out";

            // get a valid input number
            int input = PresentationHelper.MenuLoop(text, 1, 5);

            if (input == 1)
            {
                LocationModel? location = LocationMenu.SelectLocation(CurrentAccount);
                if (location is not null)
                    Reservation.GetReservation(CurrentAccount, location);
            }

            else if (input == 2)
            {
                OrderModel? order = Order.SelectOrder(CurrentAccount);
                if (order == null)
                {
                    continue;
                }

                Reservation.ManageReservations(order);
            }
            else if (input == 3)
            {
                LocationModel? location = LocationMenu.SelectLocation(CurrentAccount);
                if (location is not null)
                    Schedule.DisplaySchedule(location);
            }
            else if (input == 4)
            {
                Coupon.DisplayCoupons();
            }
            // sends the user to the start to login again
            else if (input == 5) { break; }
        }

        Start();
    }
}