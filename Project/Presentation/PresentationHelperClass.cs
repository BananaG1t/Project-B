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
            { General.PrintInRed($"That is not a valid {variableName}"); }
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
            { General.PrintInRed($"Invalid format. Please try again"); }
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
            { General.PrintInRed($"That is not a valid number"); }
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
            { General.PrintInRed($"That is not a valid number"); }
            else { break; }

        } while (true);

        return output;
    }

    public static int MenuLoop(string text, int lowerbound, int upperbound)
    {
        // create starting variables
        string input;
        int output;

        List<int> valid = [];

        for (int i = lowerbound; i < upperbound + 1; i++)
        {
            valid.Add(i);
        }

        // ask the question at least once
        Console.WriteLine(text);
        input = Console.ReadLine();

        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (!int.TryParse(input, out output) || !valid.Contains(output))
        {
            Console.Clear();
            Console.WriteLine("That is not a valid input");
            Console.WriteLine(text);
            input = Console.ReadLine();
        }

        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

    public static void PrintAndWait(string text)
    {
        Console.WriteLine(text);
        Thread.Sleep(5000);
        Console.Clear();
    }
}