static class General
{
    // method to get a valid answer from the user
    public static int ValidAnswer(string text, List<int> ValidInputs)
    {
        // create starting variables
        string input;
        int output;
        // start a do while loop to make sure the question is asked at least once
        do
        {
            Console.WriteLine(text);
            input = Console.ReadLine();
            // loop logic to make sure the input is a number and check if the number is a valid choice
        } while (!int.TryParse(input, out output) || !ValidInputs.Contains(output));
        // when it breaks out of the loop, the ouput number is valid and returns it to the method that called it
        return output;
    }
}