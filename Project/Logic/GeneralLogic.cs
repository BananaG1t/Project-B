public static class General
{
    // method to get a valid answer from the user
    public static int ValidAnswer(string text, List<int> ValidInputs)
    {
        // create starting variables
        string input;
        int output;

        // ask the question at least once
        Console.WriteLine(text);
        input = Console.ReadLine();

        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (!int.TryParse(input, out output) || !ValidInputs.Contains(output))
        {
            Console.Clear();
            Console.WriteLine("That is not a valid input");
            Console.WriteLine(text);
            input = Console.ReadLine();
        }

        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

    public static DateTime ValidDate(string text)
    {
        // create starting variables
        string input;
        DateTime output;
        string format = "dd-MM-yyyy-HH-mm";

        // ask the question at least once
        Console.WriteLine(text);
        input = Console.ReadLine();

        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (!DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out output))
        {
            Console.Clear();
            Console.WriteLine("That is not a valid input");
            Console.WriteLine(text);
            input = Console.ReadLine();
        }

        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

    public static List<int> ListMaker(int firstNumber, int lastNumber)
    {
        List<int> ValidAnswerList = [];
        for (int i = firstNumber; i < lastNumber; i++)
        {
            ValidAnswerList.Add(i);
        }
        return ValidAnswerList;
    }

    public static void PrintInRed(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}