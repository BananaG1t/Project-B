static class AddMovieMenu
{
    // Add movie menu
    public static void Main()
    {
        Console.Clear();

        string name = PresentationHelper.GetString($"Movie Name: ", "name");

        string director = PresentationHelper.GetString($"Movie Name: ", "director");

        string description = PresentationHelper.GetString($"Movie Name: ", "description");

        TimeSpan movieLength = PresentationHelper.GetTimespan($"Movie length (hh:mm): ");

        string genre = PresentationHelper.GetString($"Movie Name: ", "genre");

        int agerating = PresentationHelper.GetInt($"Movie age rating: ");

        MovieLogic.AddMovie(name, director, description, movieLength, genre, agerating, agerating);
    }

}