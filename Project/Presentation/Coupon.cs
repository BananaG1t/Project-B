public static class Coupon
{
    public static void AdminMenu()
    {
        string text =
        "Coupon Menu\n" +
        "[1] Create coupon\n" +
        "[2] Exit";

        while (true)
        {
            Console.Clear();
            int input = General.ValidAnswer(text, [1, 2]);

            if (input == 1)
            {
                CreateCoupon();
            }
            else if (input == 2)
            {
                Console.WriteLine("Exiting");
                break;
            }
        }
        Console.Clear();
        Menu.AdminMenu();
    }

    public static void CreateCoupon()
    {
        Console.Clear();
        Int64 percentage = 1;
        string couponType = "";
        double amount = 0;

        int type = General.ValidAnswer("What can the coupon be used for?\n[1] Order price\n[2] Seat reservation price\n[3] Snack reservation price",[1, 2, 3]);
        if (type == 1) couponType = "Order";
        if (type == 2) couponType = "Seats";
        if (type == 3) couponType = "Snacks";

        int input = General.ValidAnswer("Should the coupon be a percentage of the price or a fixed amount?\n[1] Percentage\n[2] Fixed amount", [1, 2]);
        if (input == 1) 
        {
            percentage = 0;
            amount = General.ValidDouble("Enter the percentage of the coupon","Invalid input. Please try again\n");
        }
        else if (input == 2)
        {
            amount = General.ValidDouble("Enter the discount price of the coupon","Invalid input. Please try again\n");
        } 
        DateTime expirationDate = ValidDate("Enter the expiration date of the coupon (dd-MM-yyyy)");

        AccountModel account = ChooseAccount();

        int couponCode = GenerateRandomCode(4);

        CouponsLogic.Write(couponCode, expirationDate, couponType, percentage, amount, account.Id);
    }

    public static int GenerateRandomCode(int length)
    {
        var numbers = new List <int>();
        Random rnd = new Random();
        for (int i = 0; i < length; i ++)
        {
            numbers.Add(rnd.Next(0, 10));
        }
        string code = string.Join("", numbers); // convert numbers into a single string
        return Convert.ToInt32(code); // convert string into a integer
    }

        public static DateTime ValidDate(string text)
    {
        // create starting variables
        string input;
        DateTime output;
        string format = "dd-MM-yyyy";

        // ask the question at least once
        Console.WriteLine(text);
        input = Console.ReadLine();

        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (!DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out output))
        {   
            Console.Clear();
            Console.WriteLine("That is not a valid input");
            Console.WriteLine(text);
            input = Console.ReadLine();
        }

        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

    public static AccountModel ChooseAccount()
    {
        Console.Clear();
        List<AccountModel> accounts = AccountsLogic.GetAllUserAccounts();
        Dictionary<int, int> ValidInputs = new Dictionary<int, int>();
        string text = "";
        int count = 0;

        foreach (AccountModel account in accounts)
        {
            count++;
            text += $"[{count}] Email: {account.EmailAddress}\n";
            ValidInputs.Add(count, (int)account.Id);
        }

        int input = General.ValidAnswer("Enter the number of the account that you want to assign the coupon to\n" + text, new List<int>(ValidInputs.Keys));
        AccountModel chosenAccount = AccountsAccess.GetById(ValidInputs[input]);
        return chosenAccount;
    }
}