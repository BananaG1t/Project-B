static class Schedule
{
    public static ScheduleModel SelectSchedule(LocationModel location)
    {
        List<ScheduleModel> Schedules = ScheduleAccess.ScheduleByDateAndLocation(location);

        string text = "what schedule do you want to see";

        for (int i = 0; i < Schedules.Count; i++)
        {
            text += $"\n[{i + 1}] Movie: {Schedules[i].Movie.Name}, Room: {Schedules[i].Auditorium.Room}, Starting time: {Schedules[i].StartTime}";

        }

        int input = PresentationHelper.MenuLoop(text, 1, Schedules.Count + 1);

        return Schedules[input - 1];
    }

    public static void CheckSchedule(AccountModel account)
    {
        PresentationHelper.Error("No locations with schedule entries");

        string confirmText =
                "There are no location with schedule entries\n" +
                "Do you want to add to the schedule?\n" +
                "[1] Yes \n" +
                "[2] No\n";

        int confirmChoice = PresentationHelper.MenuLoop(confirmText, 1, 2);
        if (confirmChoice == 1)
        {
            CreateScheduleEntry.Main(account);
        }
        else if (confirmChoice == 2)
        {
            Console.WriteLine("\nSchedule creation cancelled\n");
            Menu.AdminMenu(account);
        }
    }

    public static void DisplaySchedule(LocationModel location)
    {
        Console.Clear();

        List<ScheduleModel> Schedules = ScheduleAccess.ScheduleByDateAndLocation(location);

        // Shows what movie are playing based on the date and time and location
        Console.WriteLine($"Movies Playing");
        foreach (ScheduleModel schedule in Schedules)
        {
            Console.WriteLine(@$"Movie: {schedule.Movie.Name}, 
Room: {schedule.Auditorium.Room}, 
Starting time: {schedule.StartTime.ToString("dd-MM-yyyy HH:mm")}");
        }
        Console.WriteLine();
    }
}