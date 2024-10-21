static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            if (acc.Admin)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Manage Users (empty for now)");
                Console.WriteLine("2. System Settings (empty for now)");
                Console.WriteLine("3. Exit");
                Menu.AdminStart();
            }
            
            else 
            {
                Console.WriteLine("Welcome back " + acc.FullName);
                Console.WriteLine("Your email number is " + acc.EmailAddress);
                //Send user to main menu
            }

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }
}