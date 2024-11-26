static class AddMovieMenu
{
    // Add movie menu
    public static void Main()
    {
        Console.Clear();

        Console.WriteLine("Movie Name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Director Name: ");
        string author = Console.ReadLine();

        Console.WriteLine("Movie description: ");
        string description = Console.ReadLine();

        TimeSpan length = new TimeSpan(0, 0, 0);
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine("Movie length (hh:mm): ");
            string input = Console.ReadLine();

            valid = TimeSpan.TryParse(input, out length);

            if (!valid)
            {
                General.PrintInRed("Invalid format. Please try again");
            }
        }

        Console.WriteLine("Movie genre: ");
        string genre = Console.ReadLine();

        int agerating = 0;
        valid = false;
        while (!valid)
        {
            Console.WriteLine("Movie age rating: ");
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

        double movieRatings = 0;
        valid = false;
        while (!valid)
        {
            Console.WriteLine("Movie rating: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out movieRatings) && movieRatings >= 0)
            {
                valid = true;
            }
            else
            {
                General.PrintInRed("Invalid format. Please try again");
            }
        }
        
        // Ask admin if the movie should be added to the weekly schedule
        Console.WriteLine("Do you want to add this movie to the weekly schedule? (yes/no): ");
        string response = Console.ReadLine().ToLower();

        if (response == "yes")
        {
            Console.WriteLine("Enter the room number:");
            int room = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the start time of the movie (yyyy-MM-dd HH:mm):");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter the end time of the movie (yyyy-MM-dd HH:mm):");
            DateTime endTime = DateTime.Parse(Console.ReadLine());

            for (int day = 1; day <= 7; day++)
            {
                DateTime dayStartTime = startTime.AddDays(day - 1);
                DateTime dayEndTime = endTime.AddDays(day - 1);

                if (ScheduleAccess.IsAvailable(room, dayStartTime, dayEndTime))
                {
                    Console.WriteLine($"The movie has been added to day {day} of the weekly schedule in room {room}.");
                    MovieLogic.AddMovieByDay(day, name, author, description, length, genre, agerating, movieRatings);
                }
                else
                {
                    Console.WriteLine($"Room {room} is not available on day {day} during the specified time.");
                }
            }
        }
        else
        {
            Console.WriteLine("The movie has been added to the database but not to the weekly schedule.");
        }

    MovieLogic.AddMovie(name, author, description, length, genre, agerating, movieRatings);
    }
}
