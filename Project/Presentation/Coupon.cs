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
        
        int type = PresentationHelper.MenuLoop("What can the coupon be used for?\n[1] The price of the whole order\n[2] Seat reservation price\n[3] Snack reservation price", 1, 3);
        if (type == 1) couponType = "Order";
        if (type == 2) couponType = "Seats";
        if (type == 3) couponType = "Snacks";

        int input = PresentationHelper.MenuLoop("\nShould the coupon be a percentage of the price or a fixed amount?\n[1] Percentage\n[2] Fixed amount", 1, 2);
        if (input == 1) 
        {
            percentage = true;
            amount = CouponsLogic.ValidFloatPercentage("\nEnter the percentage of the coupon (must be between 0-100)","Invalid input. Please try again\n");
        }
        else if (input == 2)
        {
            amount = CouponsLogic.ValidFloat("\nEnter the discount price of the coupon","Invalid input. Please try again\n");
        } 
        DateTime expirationDate = PresentationHelper.ValidDate("Enter the expiration date of the coupon (dd-MM-yyyy)");

        int inputcode = PresentationHelper.MenuLoop("\ninput coupon code or random generated coupon code?\n[1] Input\n[2] Random generated", 1, 2);
        if (inputcode == 1) { couponCode = Validcode(); }
        else 
        {
            int length = PresentationHelper.GetInt("\nplease type in how long the code should be");
            couponCode = CouponsLogic.GenerateRandomCode(length);
        }


        AccountModel account = ChooseAccount();
        
        CouponsLogic.Write(couponCode, expirationDate, couponType, percentage, amount, account.Id);

        Console.WriteLine($"\nCoupon used for {couponType} expiration date: {expirationDate} coupon code: {couponCode} added and assigned to {account.EmailAddress}");
        Console.WriteLine("Press any key to go back");
        Console.ReadKey();
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
                Console.WriteLine($"[{count}]  Coupon type: {coupon.CouponType} Code: {coupon.CouponCode} discount: {PrintDiscount(coupon)} Experation date: {coupon.ExpirationDate:MM/dd/yyyy}");
            }
        Console.WriteLine("Press any key to go back");
        Console.ReadKey();
        }
    }
    public static void DisplayCoupons(int accountId)
    {
        Console.Clear();
        List<CouponModel> coupons = CouponsLogic.GetAllById(accountId);
        int count = 0;

        if (coupons == null ||coupons.Count() == 0) PresentationHelper.PrintAndWait("No coupons found");
        else
        {
            foreach (CouponModel coupon in coupons)
            {
                count++;
                Console.WriteLine($"[{count}]  Coupon type: {coupon.CouponType} Code: {coupon.CouponCode} discount: {PrintDiscount(coupon)} Experation date: {coupon.ExpirationDate:MM/dd/yyyy}");
            }
        Console.WriteLine("Press any key to go back");
        Console.ReadKey();
        }
        Console.Clear();
    }

    public static string PrintDiscount (CouponModel coupon)
    {
        if (coupon.CouponPercentage == true) { return $"% {coupon.Amount}"; }
        else { return $"€ {coupon.Amount}";}
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
                PresentationHelper.Error("Please enter a code");
            }

        } while (true);
        return code.ToUpper();
    }

        public static void UseCoupon(int accountId, string couponType, double price)
    {
        Console.Clear();
        List<CouponModel> coupons = CouponsLogic.GetAllByAccountId(accountId,couponType);
        if (coupons.Count == 0)
        {
            return;
        }
        int choice = PresentationHelper.MenuLoop("Do you want to use a coupon?\n[1] Yes\n[2] No", 1, 2);
        if (choice == 1)
        {
            string text = $"Enter the number of the coupon you want to use";
            List<int> ValidInputs = [];

            for (int i = 0; i < coupons.Count; i++)
            {
                text += $"\n[{i + 1}] Coupon type: {coupons[i].CouponType} Code: {coupons[i].CouponCode} discount: {PrintDiscount(coupons[i])}";
                ValidInputs.Add(i + 1);
            }

            int input = PresentationHelper.MenuLoop(text, 1, ValidInputs.Count);

            CouponModel usedCoupon = coupons[input - 1];

            double newPrice = CouponsLogic.CalculateDiscount(price, usedCoupon);

            if (price == 0){Console.WriteLine($"Total price: €{price} you have saved: € 0");}
            else {Console.WriteLine($"Total price: €{price} you have saved: €{price - newPrice}");}
            CouponsLogic.DeleteByCode(usedCoupon.CouponCode);
        }
    }
}