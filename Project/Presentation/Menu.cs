static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.Clear();
        // get a valid input number
        int input = General.ValidAnswer("Enter [1] to login\nEnter [2] to create new account", [1, 2]);

        if (input == 1) { UserLogin.Start(); }
        else if (input == 2)
        { //UserLogin.CreateLogin(); 
        }
    }

    public static void Main(AccountModel CurrentAccount)
    {
        string text = "Press [1] to get a new reservation\nPress [2] to see all the reservations you have made\nPress [3] to log out";
        // get a valid input number
        int input = General.ValidAnswer(text, [1, 2, 3]);

        if (input == 1)
        {
            Reservation.Start(CurrentAccount);
        }
        else if (input == 2)
        {
            // link code to see all the reservations the user has made
        }
        // sends the user to the start to login again
        else if (input == 3) { UserLogin.Start(); }
    }
}