static class LocationMenu
{
    public static void AddLocation()
    {
        Console.Clear();
        bool valid = false;
        string input = "";
        List<LocationModel> locations = LocationLogic.GetAll();

        while(!valid)
        {
            Console.WriteLine("What is the name of the new location?");
            input = Console.ReadLine();

            foreach (LocationModel location in locations)
            {
                if (input == location.Name) { Console.WriteLine("There's already an existing location with that name\n"); break; }
                else if (input == "") { Console.WriteLine("Invalid input. Please try again\n"); break; }
                else { valid = true; }
            }
        }

        LocationLogic.Add(input);
        Console.WriteLine("Added new location");
    }
}