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
        DateTime date = ValidDate("Enter the expiration date of the coupon");
        int input = General.ValidAnswer("Should the coupon be a percentage of the price or a fixed amount?\n[1] Percentage\n[2] Fixed amount", [1, 2]);

        if (input == 1) 
        {
            percentage = 0;
            ValidDouble("Enter the percentage of the coupon");
        }
        else if (input == 2)
        {
            ValidDouble("Enter the discount price of the coupon");
        } 
    }

    public static double ValidDouble(string text)
    {
        double price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine(text);
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