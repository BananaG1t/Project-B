public static class Coupon
{
    public static void AdminMenu()
    {

    }
    public static void CreateCoupon()
    {
        DateTime date = General.ValidDate("Enter the expiration date of the coupon");
        int input = General.ValidAnswer("Should the coupon be a percentage of the price or a fixed amount?\n[1] Percentage\n[2] Fixed amount", [1, 2]);

        if (input == 1) ValidDouble();
        else if (input == 2) ValidAmount();

    }
        public static int ValidAmount()
    {
        int amount = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine("Enter how many you want to buy");
            string input = Console.ReadLine();

            if (int.TryParse(input, out amount) && amount > 0)
            {
                valid = true;
            }
            else
            {
                General.PrintInRed("Invalid input. Please try again\n");
            }
        }

        return amount;
    }
    public static double ValidDouble()
    {
        double price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine("What is the price of the snack (0,0): ");
            string input = Console.ReadLine();
            
            if (input.Contains(".")) { input = input.Replace(".", ","); }

            if (double.TryParse(input, out price) && price >= 0)
            {
                valid = true;
            }
            else
            {
                General.PrintInRed("Invalid input. Please try again\n");
            }
        }

        return price;
    }

}