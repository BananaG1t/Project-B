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
        int percentage = 1;
        string couponType = "";

        int type = General.ValidAnswer("What can the coupon be used for?\n[1] Order price\n[2] Seat reservation price\n[3] Snack reservation price",[1, 2, 3]);
        if (type == 1) couponType = "Order";
        if (type == 2) couponType = "Seats";
        if (type == 3) couponType = "Snacks";

        int input = General.ValidAnswer("Should the coupon be a percentage of the price or a fixed amount?\n[1] Percentage\n[2] Fixed amount", [1, 2]);
        if (input == 1) 
        {
            percentage = 0;
            double amount = General.ValidDouble("Enter the percentage of the coupon","Invalid input. Please try again\n");
        }
        else if (input == 2)
        {
            double amount = General.ValidDouble("Enter the discount price of the coupon","Invalid input. Please try again\n");
        } 
        DateTime date = ValidDate("Enter the expiration date of the coupon (dd-MM-yyyy)");

        
    }

    public static int GenerateRandomCode(int length)
    {
        var numbers = new List <int>();
        Random rnd = new Random();
        for (int i = 0; i < length; i ++)
        {
            numbers.Add(rnd.Next(1, 1000));
        }
        string code = string.Join("", numbers); // convert numbers into a single string
        return int.Parse(code); // convert string into a integer
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

}