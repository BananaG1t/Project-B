static class LocationMenu
{
    public static void AddLocation()
    {
        Console.Clear();
        bool valid = false;
        string input = "";
        List<string> locations = LocationLogic.GetAllNames();

        while(!valid)
        {
            Console.WriteLine("What is the name of the new location?");
            input = Console.ReadLine();

            // Checks if there are any existing locations in db
            if (locations.Count() > 0)
            {  
                if (locations.Contains(input)) { Console.WriteLine("\nThere's already an existing location with that name\n"); }
                else if (input == "") { Console.WriteLine("\nInvalid input. Please try again\n"); }
                else { valid = true; }   

            }

            else 
            {
                if (input == "") { Console.WriteLine("\nInvalid input. Please try again\n"); }
                else { valid = true; }
            }
        }

        LocationLogic.Add(input);
        Console.WriteLine("\nAdded new location\n");
    }
}