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