public static class SnackReservation
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

        Console.Clear();
        Menu.AdminMenu();

    }
    public static void AddSnacks()
    {
        Console.Clear();
        string snackName = ValidName();
        double price = ValidDouble();

        SnacksLogic.Add(snackName, price);
        Console.WriteLine($"{snackName} added to the menu\n");
    }

    public static void UpdateSnacks()
    {
        Console.Clear();
        List<SnacksModel> Snacks = SnacksLogic.GetAll();
        Dictionary<int, int> ValidInputs = new Dictionary<int, int>();
        string text = "";
        int count = 0;

        foreach (SnacksModel snack in Snacks)
        {
            count++;
            text += $"[{count}] Name: {snack.Name}, Price: {snack.Price:F2}\n";
            ValidInputs.Add(count, (int)snack.Id);
        }

        int input = General.ValidAnswer(text + "Enter the number of the snack that you would like to update: ", new List<int>(ValidInputs.Keys));

        SnacksModel OldSnack = SnacksLogic.GetById(ValidInputs[input]);

        string snackName = ValidName();
        double price = ValidDouble();

        SnacksLogic.update(new SnacksModel(ValidInputs[input], snackName, price));
        Console.WriteLine($"\nChanged Name: From {OldSnack.Name} to {snackName}\n" +
        $"Changed Price: From {OldSnack.Price:F2} to {price:F2}\n");
    }

    public static void DeleteSnacks()
    {
        Console.Clear();
        List<SnacksModel> Snacks = SnacksLogic.GetAll();
        Dictionary<int, int> ValidInputs = new Dictionary<int, int>();
        string text = "";
        int count = 0;

        foreach (SnacksModel snack in Snacks)
        {
            count++;
            text += $"[{count}] Name: {snack.Name}, Price: {snack.Price:F2}\n";
            ValidInputs.Add(count, (int)snack.Id);
        }

        int input = General.ValidAnswer(text + "Enter the number of the snack that you would like to remove: ", new List<int>(ValidInputs.Keys));

        SnacksModel OldSnack = SnacksLogic.GetById(ValidInputs[input]);

        SnacksLogic.Delete(ValidInputs[input]);

        Console.Clear();
        Console.WriteLine($"Removed Name: {OldSnack.Name}, Price: {OldSnack.Price:F2} from the menu\n");
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

    public static string ValidName()
    {
        string name = "";
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine("What is the name of the snack");
            string input = Console.ReadLine();

            if (input != "")
            {
                valid = true;
                name = input;
            }
            else
            {
                General.PrintInRed("Invalid input. Please try again\n");
            }
        }

        return name;
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