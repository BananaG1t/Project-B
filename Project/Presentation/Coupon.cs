public static class Coupon
{
    public static bool IsTesting = false;
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
                DisplayCouponsAdmin();
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
        if (!IsTesting) { Console.Clear(); }
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
            amount = PresentationHelper.ValidFloatPercentage("\nEnter the percentage of the coupon (must be between 0-100)", "Invalid input. Please try again\n");
        }
        else if (input == 2)
        {
            amount = PresentationHelper.ValidFloat("\nEnter the discount price of the coupon", "Invalid input. Please try again\n");
        }
        DateTime expirationDate = PresentationHelper.ValidDate("\nEnter the expiration date of the coupon (dd-MM-yyyy)");

        int inputcode = PresentationHelper.MenuLoop("\ninput coupon code or random generated coupon code?\n[1] Input\n[2] Random generated", 1, 2);
        if (inputcode == 1) { couponCode = Validcode(); }
        else
        {
            int length = PresentationHelper.GetInt("\nplease type in how long the code should be");
            while (length < 1)
            {
                PresentationHelper.Error("Must be atleast one digit");
                length = PresentationHelper.GetInt("\nplease type in how long the code should be");
            }
            couponCode = CouponsLogic.GenerateRandomCode(length);
        }


        // AccountModel account = ChooseAccount();

        CouponsLogic.Write(couponCode, expirationDate, couponType, percentage, amount);

        Console.WriteLine($"\nCoupon used for {couponType} expiration date: {expirationDate} coupon code: {couponCode} added");
        Console.WriteLine("Press any key to go back");
        Console.ReadKey();
    }

    public static void DisplayCouponsAdmin()
    {
        Console.Clear();
        List<CouponModel> coupons = CouponsLogic.GetAll();
        int count = 0;

        if (coupons.Count() == 0)
        {
            Console.WriteLine("No available coupons");
            int input = PresentationHelper.MenuLoop("\nWould you like to create a new coupon?\n[1] Yes\n[2] No", 1, 2);
            if (input == 1) { CreateCoupon(); }
        }
        else
        {
            foreach (CouponModel coupon in coupons)
            {
                count++;
                Console.WriteLine($"[{count}]  Coupon type: {coupon.CouponType} Code: {coupon.CouponCode} discount: {PrintDiscountType(coupon)} Experation date: {coupon.ExpirationDate:MM/dd/yyyy}");
            }
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
        }
        Console.Clear();
    }

    public static void DisplayCoupons()
    {
        Console.Clear();
        List<CouponModel> coupons = CouponsLogic.GetAll();
        List<CouponModel> availableCoupons = [];
        int count = 0;
        if (coupons.Count == 0)
        {
            Console.WriteLine("No available coupons");
        }
        else
        {
            Console.WriteLine("Available coupons");
            foreach (CouponModel coupon in coupons)
            {
                if (coupon.ExpirationDate > DateTime.Now.Date)
                {
                    availableCoupons.Add(coupon);
                }
            }
            if (availableCoupons.Count == 0)
            {
                Console.WriteLine("No available coupons");
            }
            else
            {
                foreach (CouponModel availableCoupon in availableCoupons)
                {
                    count++;
                    Console.WriteLine($"[{count}] Coupon type: {availableCoupon.CouponType}, Code: {availableCoupon.CouponCode}, Discount: {PrintDiscountType(availableCoupon)}, Expiration date: {availableCoupon.ExpirationDate:dd-MM-yyyy}");
                }
            }
        }
        Console.WriteLine("Press any key to go back");
        Console.ReadKey();
        Console.Clear();
    }

    public static string PrintDiscountType(CouponModel coupon)
    {
        if (coupon.CouponPercentage == true) { return $"{coupon.Amount} Percent"; }
        else { return $" {coupon.Amount} Euros"; }
    }

    public static string Validcode()
    {
        string code;

        do
        {
            Console.WriteLine("\nPlease enter a coupon code: ");

            string input = Console.ReadLine() ?? "";

            if (input.Length > 0)
            {
                code = input;
                break;

            }
            else
            {
                PresentationHelper.Error("Please enter a code");
            }

        } while (true);
        return code.ToUpper();
    }

    public static CouponModel? SelectCoupon()
    {
        Console.Clear();
        List<CouponModel> coupons = CouponsLogic.GetAll();
        if (coupons.Count == 0)
        {
            PresentationHelper.Error("No coupons available");
            return null;
        }
        else
        {
            Console.Clear();
            string text = $"Enter the number of the coupon you want to use";
            List<int> ValidInputs = [];
            List<CouponModel> availableCoupons = new List<CouponModel>();

            for (int i = 0; i < coupons.Count; i++)
            {
                if (coupons[i].ExpirationDate > DateTime.Now.Date)
                {
                    availableCoupons.Add(coupons[i]);
                }
                if (availableCoupons.Count == 0)
                {
                    Console.WriteLine("No available coupons");
                    return null;
                }

            }
            for (int i = 0; i < availableCoupons.Count; i++)
            {

                text += $"\n[{i + 1}] Coupon type: {availableCoupons[i].CouponType} Code: {availableCoupons[i].CouponCode} discount: {PrintDiscountType(availableCoupons[i])} Expiration date: {availableCoupons[i].ExpirationDate:dd-MM-yyyy}";
                ValidInputs.Add(i + 1);
            }


            int input = PresentationHelper.MenuLoop(text, ValidInputs);
            CouponModel SelectedCoupon = coupons[input - 1];

            return SelectedCoupon;
            // CouponsLogic.DeleteByCode(usedCoupon.CouponCode);

            // else 
            // {return null;}
        }
    }

    public static void Discountprice(double price, CouponModel usedCoupon)
    {
        double newPrice = CouponsLogic.DiscountPrice(price, usedCoupon);

        if (price > 0) { Console.WriteLine($"Total {usedCoupon.CouponType} price: €{price} you have saved: €{price - newPrice}"); }
        else { Console.WriteLine($"Total {usedCoupon.CouponType} price: € 0 you have saved: € 0"); }
    }
    public static void PrintDiscount(CouponModel selectedCoupon)
    {
        Console.WriteLine($"You saved {PrintDiscountType(selectedCoupon)} off your {selectedCoupon.CouponType} price");
    }
}