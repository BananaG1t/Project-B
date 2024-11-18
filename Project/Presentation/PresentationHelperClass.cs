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

            if (int.TryParse(input, out output))
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

            if (double.TryParse(input, out output))
            { General.PrintInRed($"That is not a valid number"); }
            else { break; }

        } while (true);

        return output;
    }
}