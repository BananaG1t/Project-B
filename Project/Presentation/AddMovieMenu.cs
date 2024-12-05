static class AddMovieMenu
{
    // Add movie menu
    public static void Main()
    {
        Console.Clear();

        string name = PresentationHelper.GetString($"Movie name: ", "name");

        string director = PresentationHelper.GetString($"Director name: ", "director");

        string description = PresentationHelper.GetString($"Movie description: ", "description");

        TimeSpan movieLength = PresentationHelper.GetTimespan($"Movie length (hh:mm): ");

        string genre = PresentationHelper.GetString($"Movie genre: ", "genre");

        int agerating = PresentationHelper.GetInt($"Movie age rating: ");

        double movieRating = PresentationHelper.Getdouble("Movie rating: ");

        MovieLogic.AddMovie(name, director, description, movieLength, genre, agerating, movieRating);
    }
}
