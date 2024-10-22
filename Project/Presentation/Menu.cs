static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void AdminMenu()
    {
        //admin menu
        Console.WriteLine("Admin Menu:");
        Console.WriteLine("1. Manage Users (empty for now)");
        Console.WriteLine("2. Add a movie");
        Console.WriteLine("3. Show schedule");
        Console.WriteLine("4. Exit");


        //reading input from the menu to connect to the features
        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else if (input == "2")
        {
            Menu.AddMovieMenu();
        }
        else if (input == "3")
        {
            Menu.DisplaySchedule();
        }
        else if (input == "4")
        {
            Console.WriteLine("Exiting");
            UserLogin.Start();
        }
        
     }

    static public void Start()
    {
        Console.Clear();
        // get a valid input number
        int input = General.ValidAnswer("Enter [1] to login\nEnter [2] to create new account", [1, 2]);

        if (input == 1) { UserLogin.Start(); }
        else if (input == 2) { UserLogin.CreateLogin(); }
    }

    public static void Main(AccountModel CurrentAccount)
    {
        string text = "Press [1] to get a new reservation\nPress [2] to see all the reservations you have made\nPress [3] to log out";
        // get a valid input number
        int input = General.ValidAnswer(text, [1, 2, 3]);

        if (input == 1)
        {
            // link code to getting a reservation
        }
        
        else if (input == 2)
        {
            // link code to see all the reservations the user has made
        }
        // sends the user to the start to login again
        else if (input == 3) { UserLogin.Start(); }
    }

    // Add movie menu
    public static void AddMovieMenu()
    {
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
                Console.WriteLine("Invalid format. Please try again");
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
                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        decimal movie_ratings = 0;
        valid = false;
        while (!valid)
        {
            Console.WriteLine($"Movie rating: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out movie_ratings) && movie_ratings >= 0)
            {
                valid = true;
            }
            else
            {
                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        MovieLogic.AddMovie(name, author, description, length, genre, agerating, movie_ratings);

        Menu.AdminMenu();
    }

    public static void DisplaySchedule()
    {
        List<ScheduleModel> Schedules = [];

        // Shows what movie are playing based on the date and time
        Console.WriteLine($"Date: {Schedules[0].StartTime.ToString("d")}\nMovies Playing");
        int count = 0;
        foreach (ScheduleModel schedule in Schedules)
        {
            count++;
            Console.WriteLine($"[{count}] Movie: {schedule.Movie.Name}\n Starting time: {schedule.StartTime.ToString("HH:mm:ss")}");
        }
    }
}