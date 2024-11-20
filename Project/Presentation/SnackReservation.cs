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
        string text =
        "User menu\n" +
        "Press [1] Add snacks\n" +
        "Press [2] Delete snacks";
        int input = General.ValidAnswer(text, [1, 2]);

        if (input == 1)
        {
            string snackName;
            float price;
            do
            {
                Console.WriteLine("What snack would you like to add");
                snackName = Console.ReadLine();
                if (snackName == "1") { Menu.AdminMenu(); }
                Console.WriteLine("What is the price of the snack");
                price = float.Parse(Console.ReadLine());
                if (price == 1) { Menu.AdminMenu(); }

                if (snackName == null) { Console.WriteLine("The Snack name is invalid"); }
                else if (price < 0) { Console.WriteLine("The price is incorrect"); }
                else
                {
                    SnacksLogic.Write(new SnacksModel(snackName, price));
                    Console.WriteLine("snack added");
                }
            } while (snackName != null & price < 0);
        }
        if (input == 2)
        {
            
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