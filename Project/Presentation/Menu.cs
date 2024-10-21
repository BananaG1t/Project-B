static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void AdminMenu()
    {
        //admin menu
        Console.WriteLine("Admin Menu:");
        Console.WriteLine("1. Manage Users (empty for now)");
        Console.WriteLine("2. System Settings (empty for now)");
        Console.WriteLine("3. Exit");


        //reading input from the menu to connect to the features
        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else if (input == "2")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else if (input == "3")
        {
            Console.WriteLine("Exiting");
            UserLogin.Start();
        }

    }
}