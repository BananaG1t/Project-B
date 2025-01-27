public static class SnackReservation
{
    public static void SnackMenu()
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
            int input = PresentationHelper.MenuLoop(text, 1, 5);

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
        string text = "";

        for (int i = 0; i < Snacks.Count; i++)
        {
            text += $"[{i+1}] Name: {Snacks[i].Name}, Price: {Snacks[i].Price:F2}\n";
        }

        int input = PresentationHelper.MenuLoop(text + "Enter the number of the snack that you would like to update: ", 1, Snacks.Count);

        SnacksModel snack = Snacks[input -1];
        string oldName = snack.Name;
        double oldPrice = snack.Price;

        snack.Name = ValidName();
        snack.Price = ValidDouble();

        SnacksLogic.Update(snack);
        Console.WriteLine($"\nChanged Name: From {oldName} to {snack.Name}\n" +
        $"Changed Price: From {oldPrice:F2} to {snack.Price:F2}\n");
    }

    public static void DeleteSnacks()
    {
        Console.Clear();
        List<SnacksModel> Snacks = SnacksLogic.GetAll();
        string text = "";

        for (int i = 0; i < Snacks.Count; i++)
        {
            text += $"[{i+1}] Name: {Snacks[i].Name}, Price: {Snacks[i].Price:F2}\n";
        }

        int input = PresentationHelper.MenuLoop(text + "Enter the number of the snack that you would like to remove: ", 1, Snacks.Count);

        SnacksModel snack = Snacks[input -1];
        string oldName = snack.Name;
        double oldPrice = snack.Price;

        SnacksLogic.Delete(snack);

        Console.Clear();
        Console.WriteLine($"Removed Name: {oldName}, Price: {oldPrice:F2} from the menu\n");
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
            string input = Console.ReadLine() ?? "";

            if (input != "")
            {
                valid = true;
                name = input;
            }
            else
            {
                PresentationHelper.PrintInRed("Invalid input. Please try again\n");
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
            string input = Console.ReadLine() ?? "";

            if (input.Contains('.')) { input = input.Replace(".", ","); }

            if (double.TryParse(input, out price) && price >= 0)
            {
                valid = true;
            }
            else
            {
                PresentationHelper.PrintInRed("Invalid input. Please try again\n");
            }
        }

        return price;
    }

    public static int ValidAmount()
    {
        int amount = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine("Enter how many you want to buy");
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out amount) && amount > 0)
            {
                valid = true;
            }
            else
            {
                PresentationHelper.PrintInRed("Invalid input. Please try again\n");
            }
        }

        return amount;
    }

    public static double BuySnacks(int reservation_id, int personNum, CouponModel? coupon)
    {
        Console.Clear();
        List<SnacksModel> snacks = SnacksLogic.GetAll();
        if (snacks.Count == 0)
        {
            PresentationHelper.Error("There are no snacks to select");
            return 0;
        }

        if (coupon != null && coupon.CouponType != "Snacks" && coupon.CouponType != "Order")
        {
            coupon = null;
        }

        string discountText = coupon == null ? "" : $"Coupon applied, Discount: {(coupon.CouponPercentage ? "" : "€")}{coupon.Amount}{(coupon.CouponPercentage ? "%" : "(amount gets applied after everyone selected)")}\n";

        string text = $"{discountText}Person {personNum}, enter the number of the snack that you would like to buy";
        List<int> ValidInputs = [0];

        for (int i = 0; i < snacks.Count; i++)
        {
            double price = snacks[i].Price;
            if (coupon != null)
                if (coupon.CouponPercentage)
                {
                    price -= snacks[i].Price * coupon.Amount / 100;
                }
            text += $"\n[{i + 1}] Name: {snacks[i].Name}, Price: {price:F2}";
            ValidInputs.Add(i + 1);
        }

        double totalPrice = 0;
        double totalDisplayPrice = 0;
        while (true)
        {
            int input = PresentationHelper.MenuLoop(text + "\n[0] Done", 0, ValidInputs.Count);
            if (input == 0) return totalPrice;

            int amount = ValidAmount();

            SnacksModel boughtSnack = snacks[input - 1];
            totalPrice += amount * boughtSnack.Price;

            BoughtSnacksModel? existing = BoughtSnacksLogic.FindExisting(reservation_id, boughtSnack.Id);
            if (existing != null)
            {
                existing.Amount += amount;
                BoughtSnacksLogic.Update(existing);
            }
            else
            {
                BoughtSnacksLogic.Write(reservation_id, boughtSnack.Id, amount);
            }

            Console.Clear();
            double displayPrice = boughtSnack.Price;
            if (coupon != null)
                if (coupon.CouponPercentage)
                {
                    displayPrice -= boughtSnack.Price * coupon.Amount / 100;
                }
            totalDisplayPrice += amount * displayPrice;
            Console.WriteLine($"\nSnacks reserved: {amount} X {boughtSnack.Name}, Total Price: {totalDisplayPrice:F2}\n");
        }


    }


}