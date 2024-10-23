static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        Console.Clear();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);

            if (acc.Admin)
            {
                Menu.AdminMenu();
            }
            else
            {
                Menu.Main(acc);
            }


            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");// added create new account option in main menu.
            int input = General.ValidAnswer("Would you like to make a new account?\n[1] Yes\n[2] No", [1, 2]);
            if (input == 1) { CreateLogin(); }
            else if (input == 2) { Menu.Start(); }
        }
    }
    public static void CreateLogin()
    {
        Console.Clear();
        string email;
        string password;
        string? fullname;

        do
        {
            Console.WriteLine("Welcome to the account creation page");
            Console.WriteLine("Enter [1] to return to menu");
            Console.WriteLine("Please enter your full name (Optional)");
            fullname = Console.ReadLine();
            if (fullname == "1") Menu.Start();
            Console.WriteLine("Please enter your email address");
            email = Console.ReadLine();
            if (email == "1") Menu.Start();
            Console.WriteLine("Please enter your password");
            password = Console.ReadLine();
            if (password == "1") Menu.Start();
            Console.Clear();
        } while (!accountsLogic.Validinfo(email, password));

        fullname = fullname == "" ? null : fullname;
        AccountModel newacc = new AccountModel(email, password, fullname);
        Console.WriteLine($"Welcome {newacc.FullName}");
        Console.WriteLine($"Your email number is {newacc.EmailAddress}");
        Menu.Main(newacc);
    }
}
