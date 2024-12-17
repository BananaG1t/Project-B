public static class PresentationHelper
{
    public static string GetString(string text, string variableName)
    {
        string output = "";

        do
        {
            Console.WriteLine(text);
            output = Console.ReadLine();

            if (output.Count() == 0)
            { PrintInRed($"That is not a valid {variableName}"); }
            else { break; }

        } while (true);

        return output;
    }

    public static TimeSpan GetTimespan(string text)
    {
        TimeSpan ValidTimeSpan = new TimeSpan(0, 0, 0);

        string output = "";

        do
        {
            Console.WriteLine(text);
            output = Console.ReadLine();

            if (!TimeSpan.TryParse(output, out ValidTimeSpan))
            { PrintInRed($"Invalid format. Please try again"); }
            else { break; }

        } while (true);

        return ValidTimeSpan;
    }

    public static int GetInt(string text)
    {
        int output;

        do
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();

            if (!int.TryParse(input, out output))
            { PrintInRed($"That is not a valid number"); }
            else { break; }

        } while (true);

        return output;
    }

    public static double Getdouble(string text)
    {
        double output;

        do
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();

            if (!double.TryParse(input, out output))
            { PrintInRed($"That is not a valid number"); }
            else { break; }

        } while (true);

        return output;
    }

    // turn the lowerbound / upperbound into a list and give it to the main menuloop
    public static int MenuLoop(string text, int lowerbound, int upperbound) => MenuLoop(text, Enumerable.Range(lowerbound, upperbound).ToList());

    public static int MenuLoop(string text, List<int> valid)
    {
        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (true)
        {
            // print the question
            Console.WriteLine(text);

            // ask the user for input and check if its and int
            if (!int.TryParse(Console.ReadLine(), out int output))
            {
                Error("Must enter a number");
                continue;
            }

            if (!valid.Contains(output))
            {
                Error("Input is not valid");
                continue;
            }

            // when it passes all the test return the output
            return output;
        }
    }

    public static void PrintAndEnter(string text)
    {
        Console.WriteLine(text);
        Console.WriteLine("Press enter to continue");
        Console.ReadLine();
    }

    public static void PrintInRed(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Error(string message)
    {
        Console.Clear();
        PrintInRed(message);
    }

}