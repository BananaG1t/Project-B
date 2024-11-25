static class SnackReservation
{
    public static void Main()
    {
        string text =
        "Snack menu\n" +
        "[1] Add snacks\n" +
        "[2] Update snacks\n" +
        "[3] Delete snacks\n" +
        "[4] Display snacks\n" +
        "[5] Go back to admin menu";

        while (true)
        {
            int input = General.ValidAnswer(text, [1, 2, 3, 4, 5]);

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
                DeleteSnacks();
            }
            else if (input == 4)
            {
                DisplaySnacks();
            }
            else if (input == 5)
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
            Console.Clear();
            Console.WriteLine("What snack would you like to add");
            snackName = Console.ReadLine();


            price = ValidDouble();


            if (snackName == null) { Console.WriteLine("The Snack name is invalid"); }
            else if (price < 0) { Console.WriteLine("The price is incorrect"); }
            else
            {
                SnacksLogic.Add(snackName, price);
                Console.WriteLine($"{snackName} added to the menu");
                break;
            }
        } while (snackName == null || price < 0);
    }

    public static void UpdateSnacks()
    {
        Console.Clear();
        List<SnacksModel> Snacks = SnacksLogic.GetAll();
        List<int> ValidInputs = [];
        string text = "";

        foreach (SnacksModel snack in Snacks)
        {
            text += $"[{snack.Id}] Name: {snack.Name}, Price: {snack.Price:F2}\n";
            ValidInputs.Add((int)snack.Id);
        }

        int input = General.ValidAnswer(text + "Enter the number of the snack that you would like to update: ", ValidInputs);

        SnacksModel OldSnack = SnacksLogic.GetById(input);

        string snackName;
        double price;
        do
        {
            Console.Clear();
            Console.WriteLine("What is the name of the new snack");
            snackName = Console.ReadLine();
            if (snackName == null) { Console.WriteLine("The Snack name is invalid"); }

            price = ValidDouble();

        } while (snackName == null || price < 0);

        SnacksLogic.update(new SnacksModel(input, snackName, price));
        Console.WriteLine($"Changed Name: From {OldSnack.Name} to {snackName}\n" +
        $"Changed Price: From {OldSnack.Price:F2} to {price:F2}");
    }

    public static void DeleteSnacks()
    {
        Console.Clear();
        List<SnacksModel> Snacks = SnacksLogic.GetAll();
        List<int> ValidInputs = [];
        string text = "";

        foreach (SnacksModel snack in Snacks)
        {
            text += $"[{snack.Id}] Name: {snack.Name}, Price: {snack.Price:F2}\n";
            ValidInputs.Add((int)snack.Id);
        }

        int input = General.ValidAnswer(text + "Enter the number of the snack that you would like to remove: ", ValidInputs);

        SnacksModel OldSnack = SnacksLogic.GetById(input);

        SnacksLogic.Delete(input);

        Console.WriteLine($"Removed Name: {OldSnack.Name}, Price: {OldSnack.Price:F2} from the menu");
    }

    public static void DisplaySnacks()
    {
        Console.Clear();
        List<SnacksModel> Snacks = SnacksLogic.GetAll();
        Console.WriteLine($"Available snacks:");
        foreach (SnacksModel snack in Snacks)
        {
            Console.WriteLine($"Name: {snack.Name}, Price: {snack.Price:F2}");
        }
        Console.WriteLine();
    }

    private static double ValidDouble()
    {
        double price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.Clear();
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

    public static void BuySnacks()
    {
        Console.Clear();
        List<SnacksModel> Snacks = SnacksLogic.GetAll();
        List<int> ValidInputs = [];
        string text = "";

        foreach (SnacksModel snack in Snacks)
        {
            text += $"[{snack.Id}] Name: {snack.Name}, Price: {snack.Price:F2}\n";
            ValidInputs.Add((int)snack.Id);
        }

        int input = General.ValidAnswer(text + "Enter the number of the snack you would like to buy: ", ValidInputs);
        

    }


}