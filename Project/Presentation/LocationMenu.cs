static class LocationMenu
{
    public static void Main()
    {
        string text =
        "Locations menu:\n" +
        "[1] Add location\n" +
        "[2] Update location\n" +
        "[3] Delete location\n" +
        "[4] Display locations\n" +
        "[5] Go back to admin menu";

        while (true)
        {
            int input = PresentationHelper.MenuLoop(text, 1, 5);

            if (input == 1)
            {
                AddLocation();
            }
            else if (input == 2)
            {
                UpdateLocation();
            }
            else if (input == 3)
            {
                DeleteLocation();
            }
            else if (input == 4)
            {
                DisplayLocations();
            }
            else if (input == 5)
            {
                Console.WriteLine("Exiting");
                break;
            }
        }

        Console.Clear();
    }
    public static void AddLocation()
    {
        Console.Clear();
        bool valid = false;
        List<string> locations = LocationLogic.GetAllNames();
        string input;

        do 
        {
            Console.WriteLine("What is the name of the new location?");
            input = Console.ReadLine() ?? "";

            // Checks if there are any existing locations in db
            if (locations.Count() > 0)
            {
                if (locations.Contains(input)) { PresentationHelper.Error("\nThere's already an existing location with that name\n"); }
                else if (input == "") { PresentationHelper.Error("\nInvalid input. Please try again\n"); }
                else { valid = true; }

            }

            else
            {
                if (input == "") { PresentationHelper.Error("\nInvalid input. Please try again\n"); }
                else { valid = true; }
            }
        } while (!valid);

        LocationLogic.Add(input);
        Console.WriteLine("\nAdded new location\n");
    }

    public static void UpdateLocation()
    {
        Console.Clear();
        string text = "Which location do you want to update?";
        List<LocationModel> locations = LocationLogic.GetAll();
        if (locations.Count > 0)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                text += $"\n[{i + 1}] {locations[i].Name}";

            }
            int LocationId = PresentationHelper.MenuLoop(text, 1, locations.Count);
            LocationModel OldLocation = locations[LocationId - 1];

            bool valid = false;
            string name = "";

            while (!valid)
            {
                Console.WriteLine("What is the name of the new location?");
                name = Console.ReadLine() ?? "";

                if (name == "") { Console.WriteLine("\nInvalid input. Please try again\n"); }
                else { valid = true; }
            }

            LocationLogic.Update(new LocationModel(OldLocation.Id, name));
            Console.WriteLine($"\nChanged Location Name: From \"{OldLocation.Name}\" to \"{name}\"\n");
        }

        else
        {
            Console.WriteLine("There are no locations to update\n");
        }
    }

    public static void DeleteLocation()
    {
        Console.Clear();
        List<LocationModel> locations = LocationLogic.GetAll();
        if (locations.Count > 0)
        {
            string text = "Which location do you want to remove?";

            for (int i = 0; i < locations.Count; i++)
            {
                text += $"\n[{i + 1}] {locations[i].Name}";

            }
            int LocationId = PresentationHelper.MenuLoop(text, 1, locations.Count);
            LocationModel OldLocation = locations[LocationId - 1];

            PresentationHelper.PrintInRed("\nWARNING! Removing a location will delete everything associated with the location\n");
            string confirmText =
                    "Are you sure you want to remove this location?\n" +
                    "[1] Yes \n" +
                    "[2] No\n";

            int confirmChoice = PresentationHelper.MenuLoop(confirmText, 1, 2);

            if (confirmChoice == 1)
            {
                LocationLogic.Delete(OldLocation.Id);
                Console.WriteLine($"\nRemoved \"{OldLocation.Name}\"\n");
            }
            else
            {
                Console.WriteLine("\nRemoving of location aborted\n");
            }
        }

        else
        {
            Console.WriteLine("There are no locations to remove\n");
        }

    }

    public static void DisplayLocations()
    {
        Console.Clear();
        List<LocationModel> locations = LocationLogic.GetAll();

        if (locations.Count > 0)
        {
            Console.WriteLine("All current locations:");
            foreach (LocationModel location in locations)
            {
                Console.WriteLine(location.Name);
            }
            Console.WriteLine();
        }

        else
        {
            Console.WriteLine("There are no locations to display\n");
        }
    }
    public static LocationModel? SelectLocation(AccountModel account, bool canAdd = false, bool addSchedule = false)
    {
        Console.Clear();
        string text = "At which location do you want to see?";
        List<LocationModel> locations = LocationLogic.GetAll();
        List<LocationModel> ScheduleLocations = ScheduleAccess.GetAllLocationsWithSchedules();
        List<LocationModel> NoScheduleLocations = LocationLogic.GetAllLocationsWithNoSchedules();

        if (locations.Count == 0)
        {
            PresentationHelper.Error("No locations found");
            if (!canAdd) return null;

            string confirmText =
                    "There are no locations\n" +
                    "Do you want to add a new location?\n" +
                    "[1] Yes \n" +
                    "[2] No\n";

            int confirmChoice = PresentationHelper.MenuLoop(confirmText, 1, 2);
            if (confirmChoice == 1)
            {
                AddLocation();
                ScheduleLocations = ScheduleAccess.GetAllLocationsWithSchedules();
            }
            else if (confirmChoice == 2)
            {
                Console.WriteLine("\nReturning to admin menu\n");
                return null;
            }
        }

        if (canAdd && addSchedule)
        {
                locations = LocationLogic.GetAll();
                for (int i = 0; i < locations.Count; i++)
                {
                    text += $"\n[{i + 1}] {locations[i].Name}";
                }
                int LocationId = PresentationHelper.MenuLoop(text, 1, locations.Count);
                LocationModel Location = locations[LocationId - 1];
                return Location;
        }

        if (ScheduleLocations.Count == 0)
        {
            if (!canAdd)
            {
                PresentationHelper.Error("No locations with schedules found");
                return null;
            }   
            Schedule.CheckSchedule(account);
            ScheduleLocations = ScheduleAccess.GetAllLocationsWithSchedules();
            NoScheduleLocations = LocationLogic.GetAllLocationsWithNoSchedules();
        }

        NoScheduleLocations = NoScheduleLocations.Except(ScheduleLocations).ToList();

        // Adds all locations with schedules to dict and as a valid option for reserving
        for (int i = 0; i < ScheduleLocations.Count; i++)
        {
            text += $"\n[{i + 1}] {ScheduleLocations[i].Name}";
        }

        // Adds all locations with no schedules to dict without adding it as a valid option for reserving
        for (int i = 0; i < NoScheduleLocations.Count; i++)
        {
            text += $"\n{NoScheduleLocations[i].Name} (Coming Soon!)";
        }

        int locationId = PresentationHelper.MenuLoop(text, 1, ScheduleLocations.Count);
        return ScheduleLocations[locationId - 1];
    }
}