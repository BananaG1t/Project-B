static class SnackReservation
{
    public static void ManageSnacks()
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
                SnacksAcces.Write(new SnacksModel(snackName, price));
                Console.WriteLine("snack added");
            }
        } while (snackName != null & price < 0 );
    }
}