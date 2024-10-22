static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to do something else in the future");

        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }
    }

    public static void Main(AccountModel CurrentAccount)
    {
        List<string> ValidChoices = ["1", "2"];
        string choice;
        do
        {
            Console.WriteLine("Press [1] to get a new reservation");
            Console.WriteLine("Press [2] to see all the reservations you have made");

            choice = Console.ReadLine();
            if (choice == "1")
            {
                // link code to getting a reservation
            }
            else if (choice == "2")
            {
                // link code to see all the reservations the user has made
            }
            else { Console.WriteLine("That is not a valid choice!"); }
        } while (!ValidChoices.Contains(choice));

    }
}