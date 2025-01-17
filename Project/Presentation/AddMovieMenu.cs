static class AddMovieMenu
{
    // Add movie menu
    public static void AddMovie()
    {
        Console.Clear();

        string name = PresentationHelper.GetString($"Movie name: ", "name");

        string director = PresentationHelper.GetString($"Director name: ", "director");

        string description = PresentationHelper.GetString($"Movie description: ", "description");

        TimeSpan movieLength = PresentationHelper.GetTimespan($"Movie length (hh:mm): ");

        string genre = PresentationHelper.GetString($"Movie genre: ", "genre");

        int ageRating = PresentationHelper.GetInt($"Movie age rating: ");
        while (ageRating < 0)
        {
            PresentationHelper.Error("Age rating must be a positive number");
            ageRating = PresentationHelper.GetInt($"Movie age rating: ");
        }

        double movieRating = PresentationHelper.Getdouble("Movie rating (0 - 5): ");
        while (movieRating < 0 || movieRating > 5)
        {
            PresentationHelper.Error("Rating must be between 0 and 5");
            movieRating = PresentationHelper.Getdouble("Movie rating (0 - 5): ");
        }

        MovieLogic.AddMovie(name, director, description, movieLength, genre, ageRating, movieRating);
    }
}
