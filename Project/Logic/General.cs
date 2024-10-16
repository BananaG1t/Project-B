static class General
{
    public static int ValidAnswer(string text, List<int> ValidInputs)
    {
        string input;
        int output;
        do
        {
            Console.WriteLine(text);
            input = Console.ReadLine();
        } while (!int.TryParse(input, out output) || !ValidInputs.Contains(output));

        return output;
    }
}