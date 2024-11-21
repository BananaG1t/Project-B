static class SnackReservation
{
    public static void Main()
    {
        string text =
        "Snack menu\n" +
        "[1] Add snacks\n" +
        "[2] Update snacks\n" +
        "[3] Delete snacks\n" +
        "[4] Go back to admin menu";

        while (true)
        {
            int input = General.ValidAnswer(text, [1, 2, 3]);

            if (input == 1)
            {
                AddSnacks();
            }
            else if (input == 2)
            {
               UpdateSnacks();
            }
            else if (input == 3)
            {
                Console.WriteLine("This feature is not yet implemented");
            }
            else if (input == 4)
            {
                Console.WriteLine("Exiting");
                break;
            }
        }

        Menu.AdminMenu();

    }
    public static void AddSnacks()
    {
        string snackName;
        double price;

        do
        {
            Console.WriteLine("What snack would you like to add");
            snackName = Console.ReadLine();

            // if (snackName == "1") { Menu.AdminMenu(); }

            price = ValidDouble();

            // if (price == 1) { Menu.AdminMenu(); }

            if (snackName == null) { Console.WriteLine("The Snack name is invalid"); }
            else if (price < 0) { Console.WriteLine("The price is incorrect"); }
            else
            {
                SnacksLogic.Add(snackName, price);
                Console.WriteLine($"{snackName} added to the menu");
            }
        } while (snackName != null & price < 0);
    }

    public static void UpdateSnacks()
    {
        List<SnacksModel> Snacks = SnacksLogic.GetAll();

        foreach (SnacksModel snack in Snacks)
        {
            Console.WriteLine($"Name: {snack.Name} Price: {snack.Price}");
        }
    }

    private static double ValidDouble()
    {
        double price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine("What is the price of the snack (0,0): ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out price))
            {
                valid = true;
            }
            else
            {
                General.PrintInRed("Invalid input. Please try again");
            }
        }

        return price;
    }
}