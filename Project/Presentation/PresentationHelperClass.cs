public static class PresentationHelper
{
    // this for only to ignore the exception caused by Console.Clear() in the test environment
    public static bool IsTesting { get; set; } = false;
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
            string input = Console.ReadLine().Replace(".", ",");

            if (!double.TryParse(input, out output))
            { PrintInRed($"That is not a valid number"); }
            else { break; }

        } while (true);

        return output;
    }

    // turn the lowerbound / upperbound into a list and give it to the main menuloop
    public static int MenuLoop(string text, int lowerbound, int upperbound)
    {
        if (lowerbound > upperbound) { throw new ArgumentException("Lowerbound cannot be higher than upperbound"); }
        if (lowerbound == 0 && upperbound > 0) { upperbound++; }
        return MenuLoop(text, Enumerable.Range(lowerbound, upperbound).ToList());
    }

    public static int MenuLoop(string text, List<int> valid)
    {
        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (true)
        {
            if (!IsTesting)
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
            
            else
            {
               // print the question
                Console.WriteLine(text);

                // ask the user for input and check if its and int
                if (!int.TryParse(Console.ReadLine(), out int output))
                {
                    Error("Must enter a number");
                    return 0;
                }

                if (!valid.Contains(output))
                {
                    Error("Input is not valid");
                    return 0;
                }

                // when it passes all the test return the output
                return output; 
            }
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
        if (!IsTesting)
        {
            Console.Clear();
        }
        PrintInRed(message);
    }
    public static DateTime ValidDate(string text, string format = "dd-MM-yyyy")
    {
        // create starting variables
        DateTime output;
        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (true)
        {
            Console.WriteLine(text);
            if (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out output))
            {
                Error("Not a valid datetime format");
                continue;
            }

            if (output < DateTime.Now.Date)
            {
                Error("Date cannot be in the past");
                continue;
            }
            break;
        }
        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

    public static float ValidFloat(string text, string errorText)
    {
        float price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();

            if (input.Contains(",")) { input = input.Replace(",", "."); }

            if (float.TryParse(input, out price) && price > 0)
            {
                valid = true;
            }
            else
            {
                Error(errorText);
            }
        }
        float roundedPrice = (float)Math.Round(price, 2);
        return roundedPrice;
    }

    public static float ValidFloatPercentage(string text, string errorText)
    {
        float price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();

            if (input.Contains(",")) { input = input.Replace(",", "."); }

            if (float.TryParse(input, out price) && price > 0 && price <= 100)
            {
                valid = true;
            }
            else
            {
                Error(errorText);
            }
        }
        float roundedPrice = (float)Math.Round(price, 2);
        return roundedPrice;
    }
}