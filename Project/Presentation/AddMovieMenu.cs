static class AddMovieMenu
{
    // Add movie menu
    public static void Main()
    {
        Console.Clear();

        Console.WriteLine($"Movie Name: ");
        string name = Console.ReadLine();

        Console.WriteLine($"Author Name: ");
        string author = Console.ReadLine();

        Console.WriteLine($"Movie description: ");
        string description = Console.ReadLine();

        TimeSpan length = new TimeSpan(0, 0, 0);
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine($"Movie length (hh:mm:ss): ");
            string input = Console.ReadLine();

            valid = TimeSpan.TryParse(input, out length);

            if (!valid)
            {
                General.PrintInRed("Invalid format. Please try again");
            }
        }

        Console.WriteLine($"Movie genre: ");
        string genre = Console.ReadLine();


        int agerating = 0;
        valid = false;
        while (!valid)
        {
            Console.WriteLine($"Movie age rating: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out agerating) && agerating >= 0)
            {
                valid = true;
            }
            else
            {
                General.PrintInRed("Invalid format. Please try again");
            }
        }

        double movie_ratings = 0;
        valid = false;
        while (!valid)
        {
            Console.WriteLine($"Movie rating: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out movie_ratings) && movie_ratings >= 0)
            {
                valid = true;
            }
            else
            {
                General.PrintInRed("Invalid format. Please try again");
            }
        }

        MovieLogic.AddMovie(name, author, description, length, genre, agerating, movie_ratings);
    }

}