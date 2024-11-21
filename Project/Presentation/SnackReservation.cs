static class SnackReservation
{
    public static void Main()
    {
        string text =
        "Snack menu\n" +
        "[1] Add snacks\n" +
        "[2] Delete snacks\n" +
        "[3] Go back to admin menu";

        while (true)
        {
            int input = General.ValidAnswer(text, [1, 2, 3]);

            if (input == 1)
            {
                ManageSnacks();
            }
            else if (input == 2)
            {
                Console.WriteLine("This feature is not yet implemented");
            }
            else if (input == 3)
            {
                Console.WriteLine("Exiting");
                break;
            }
        }

        Menu.AdminMenu();

    }
    public static void ManageSnacks()
    {
        string snackName;
        double price;

        do
        {
            Console.WriteLine("What snack would you like to add");
            snackName = Console.ReadLine();

            // if (snackName == "1") { Menu.AdminMenu(); }

            Console.WriteLine("What is the price of the snack");
            price = ValidDouble();

            // if (price == 1) { Menu.AdminMenu(); }

            if (snackName == null) { Console.WriteLine("The Snack name is invalid"); }
            else if (price < 0) { Console.WriteLine("The price is incorrect"); }
            else
            {
                SnacksLogic.Write(new SnacksModel(snackName, price));
                Console.WriteLine($"{snackName} added to the menu");
            }
        } while (snackName != null & price < 0);
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