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

            if (RoleLogic.HasAccess(acc, 1))
            {
                Menu.AdminMenu(acc);
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
            string text = "Would you like to make a new account?\n[1] Yes\n[2] No";
            int input = PresentationHelper.MenuLoop(text, 1, 2);

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

        int valid;
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
            valid = accountsLogic.Validinfo(email, password);
            if (valid == 1) { PresentationHelper.Error("Invalid email"); }
            else if (valid == 2) { PresentationHelper.Error("Account with this email already exists"); }
            else if (valid == 3) { PresentationHelper.Error("Invalid password"); }
        } while (valid != 0);

        fullname = fullname == "" ? null : fullname;
        AccountModel newacc = new AccountModel(email, password, fullname);
        Console.WriteLine($"Welcome {newacc.FullName}");
        Console.WriteLine($"Your email number is {newacc.EmailAddress}");
        Menu.Main(newacc);
    }
}
