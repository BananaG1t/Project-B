public static class General
{
    public static DateTime ValidDate(string text, string errorText)
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
            Console.WriteLine(errorText);
            Console.WriteLine(text);
            input = Console.ReadLine();
        }

        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

        public static DateTime ValidDate(string text, string pastErrorText,string errorText )
    {
        // create starting variables
        string input;
        DateTime output;
        string format = "dd-MM-yyyy";

        Console.WriteLine(text);
        input = Console.ReadLine();

        // loop logic to make sure the input is a number and check if the number is a valid choice
        while (!DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out output) || output < DateTime.Now.Date)
        {
            if (output < DateTime.Now.Date)
            {
                Console.Clear();
                Console.WriteLine(pastErrorText);
            }
            else
            {
                Console.Clear();
                PresentationHelper.Error(errorText);
            }
                Console.WriteLine(text);
                input = Console.ReadLine();
        }

        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }

}
