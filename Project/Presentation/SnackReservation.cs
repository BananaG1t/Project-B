static class SnackReservation
{
    public static void ManageSnacks()
    {
        string snackName;
        double price;
        do
        {
            Console.WriteLine("What snack would you like to add");
            snackName = Console.ReadLine();
            if (snackName == "1") { Menu.AdminMenu(); }

            price = ValidDouble();

            if (snackName == null) { Console.WriteLine("The Snack name is invalid"); }
            else if (price < 0) { Console.WriteLine("The price is incorrect"); }
            else
            {
                SnacksAcces.Write(new SnacksModel(snackName, price));
                Console.WriteLine("snack added");
            }
        } while (snackName != null & price < 0 );
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