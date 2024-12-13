using System.ComponentModel.DataAnnotations;

public static class Coupon
{
    public static void AdminMenu()
    {
        string text =
        "Coupon Menu\n" +
        "[1] Create coupon\n" +
        "[2] Display coupons\n" +
        "[3] Exit";

        while (true)
        {
            Console.Clear();
            int input = PresentationHelper.MenuLoop(text, [1, 2, 3]);

            if (input == 1)
            {
                CreateCoupon();
            }
            if (input == 2)
            {
                DisplayCoupons();
            }
            else if (input == 3)
            {
                Console.WriteLine("Exiting");
                break;
            }
        }
        Console.Clear();
    }

    public static void CreateCoupon()
    {
        Console.Clear();
        bool percentage = false;
        string couponType = "";
        float amount = 0;
        string couponCode;
        
        int type = PresentationHelper.MenuLoop("What can the coupon be used for?\n[1] Order price\n[2] Seat reservation price\n[3] Snack reservation price", 1, 3);
        if (type == 1) couponType = "Order";
        if (type == 2) couponType = "Seats";
        if (type == 3) couponType = "Snacks";

        int input = PresentationHelper.MenuLoop("\nShould the coupon be a percentage of the price or a fixed amount?\n[1] Percentage\n[2] Fixed amount", 1, 2);
        if (input == 1) 
        {
            percentage = true;
            amount = ValidFloatPercentage("\nEnter the percentage of the coupon (must be between 0-100)","Invalid input. Please try again\n");
        }
        else if (input == 2)
        {
            amount = ValidFloat("\nEnter the discount price of the coupon","Invalid input. Please try again\n");
        } 
        DateTime expirationDate = ValidDate("\nEnter the expiration date of the coupon (dd-MM-yyyy)");

        int inputcode = PresentationHelper.MenuLoop("\ninput coupon code or random generated coupon code?\n[1] Input\n[2] Random generated", 1, 2);
        if (inputcode == 1) { couponCode = Validcode(); }
        else 
        {
            int length = PresentationHelper.GetInt("\nplease type in how long the code should be");
            couponCode = GenerateRandomCode(length);
        }


        AccountModel account = ChooseAccount();
        
        CouponsLogic.Write(couponCode, expirationDate, couponType, percentage, amount, account.Id);

        Console.WriteLine($"\nCoupon used for {couponType} expiration date: {expirationDate} coupon code: {couponCode} added and assigned to {account.EmailAddress}");
        Console.WriteLine("ENTER To go back");
        Console.ReadLine();
    }

    public static void DisplayCoupons()
    {
        Console.Clear();
        List<CouponModel> coupons = CouponsLogic.GetAll();
        int count = 0;

        if (coupons.Count() == 0) PresentationHelper.PrintAndWait("No coupons found");
        else
        {
            foreach (CouponModel coupon in coupons)
            {
                count++;
                Console.WriteLine($"[{count}]  Coupon type: {coupon.CouponType} Code: {coupon.CouponCode} discount: {PrintDiscount(coupon),5:C}");
            }

            Console.WriteLine("\n[ENTER] To go back");
            Console.ReadLine();
        }
    }

    public static string PrintDiscount (CouponModel coupon)
    {
        if (coupon.CouponPercentage == true) { return $"% {coupon.Amount}"; }
        else { return $"€ {coupon.Amount}";}
    }

    public static string GenerateRandomCode(int length)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] code = new char [length];

        for (int i = 0; i < length; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        return new string(code);
    }

    public static DateTime ValidDate(string text)
    {
        // create starting variables
        string input;
        DateTime output;
        string format = "dd-MM-yyyy";

        Console.WriteLine(text);
        input = Console.ReadLine();

        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (!DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out output) || output < DateTime.Now.Date)
        {
            if (output < DateTime.Now.Date)
            {
                Console.Clear();
                Console.WriteLine("The date cannot be in the past");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("That is not a valid input");
            }
                Console.WriteLine(text);
                input = Console.ReadLine();
        }

        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

    public static AccountModel ChooseAccount()
    {
        Console.Clear();
        AccountsLogic accountsLogic = new AccountsLogic();
        List<AccountModel> accounts = accountsLogic.GetAllAccounts();
        Dictionary<int, int> ValidInputs = new Dictionary<int, int>();
        string text = "";
        int count = 0;

        foreach (AccountModel account in accounts)
        {
            count++;
            text += $"[{count}] Email: {account.EmailAddress}\n";
            ValidInputs.Add(count, (int)account.Id);
        }

        int input = PresentationHelper.MenuLoop("Enter the number of the account that you want to assign the coupon to\n" + text, new List<int>(ValidInputs.Keys));
        AccountModel chosenAccount = AccountsAccess.GetById(ValidInputs[input]);
        return chosenAccount;
    }
    public static float ValidFloatPercentage(string text, string errorText)
    {
        float price = 0;
        bool valid = false; 
        while (!valid)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();
            
            if (input.Contains(",")) { input = input.Replace(",", "."); }

            if (float.TryParse(input, out price) && price > 0 && price <= 100)
            {
                valid = true;
            }
            else
            {
                Console.WriteLine(errorText);
            }
        }
        float roundedPrice = (float)Math.Round(price, 2);
        return roundedPrice;
    }
    public static float ValidFloat(string text, string errorText)
    {
        float price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();
            
            if (input.Contains(",")) { input = input.Replace(",", "."); }

            if (float.TryParse(input, out price) && price > 0)
            {
                valid = true;
            }
            else
            {
                Console.WriteLine(errorText);
            }
        }
        float roundedPrice = (float)Math.Round(price, 2); 
        return roundedPrice;
    }
    public static string Validcode()
    {
        string code;
        do
        {
            Console.WriteLine("\nPlease enter a coupon code: ");

            string input = Console.ReadLine();

            if (input.Length >= 0)
            {
                code = input;
                break;

            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nPlease enter a code");
            }

        } while (true);

        
        return code.ToUpper();
    }
}