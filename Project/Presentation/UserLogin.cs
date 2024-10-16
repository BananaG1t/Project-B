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
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
            Console.WriteLine("Would you like to make a new account?\n[1] Yes\n[2] No"); // added create new account option in main menu.
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    {
                        CreateLogin();
                        break;
                    }
                case "2":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
            }
        }
    }
    public static void CreateLogin()
    {
        string email;
        string password;
        string? fullname;
        do
        {
            Console.WriteLine("Please enter your full name (Optional)");
            fullname = Console.ReadLine();
            Console.WriteLine("Please enter your email address");
            email = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            password = Console.ReadLine();

            if (!email.Contains("@") & !email.Contains(".")) Console.WriteLine("Invalid email");
            else if (password.Length < 1) Console.WriteLine("Invalid password");
            else if (!accountsLogic.CheckNewEmail(email))
            {
                Console.WriteLine("Account with this email already exists");
                continue;
            }
            else Console.WriteLine("New account added");

        } while (!email.Contains("@") & !email.Contains(".") & password.Length < 1);

        fullname = fullname == "" ? fullname : null;
        AccountModel newacc = new AccountModel(email, password, fullname);

    }
}
