static class Schedule
{
    public static int SelectDay(List<IGrouping<DateTime, ScheduleModel>> scheduleByDay)
    {
        int dayIndex;
        string text = "Select a day to view the schedule";

        for (int i = 0; i < scheduleByDay.Count; i++)
        {
            text += $"\n[{i + 1}] {scheduleByDay[i].Key:dd-MM-yy}";
        }

        text += "\n[0] Back";
        dayIndex = PresentationHelper.MenuLoop(text, 0, scheduleByDay.Count);

        if (dayIndex == 0)
            return -1;

        return dayIndex - 1;
    }

    public static ScheduleModel? SelectSchedule(LocationModel location)
    {
        List<ScheduleModel> schedules = ScheduleAccess.ScheduleByDateAndLocation(location);
        if (schedules.Count == 0)
        {
            PresentationHelper.Error("No schedules found");
            return null;
        }

        var scheduleByDay = ScheduleLogic.GroupByDay(schedules);
        int dayIndex = SelectDay(scheduleByDay);

        if (dayIndex == -1)
            return null;

        schedules = scheduleByDay[dayIndex].ToList();
        string text = $"Select a schedule";

        for (int i = 0; i < schedules.Count; i++)
        {
            text += $"\n[{i + 1}] Movie: {schedules[i].Movie.Name}, Room: {schedules[i].Auditorium.Room}, Starting time: {schedules[i].StartTime}";
        }

        text += "\n[0] Back";
        int input = PresentationHelper.MenuLoop(text, 0, schedules.Count);

        return input == 0 ? null : schedules[input - 1];
    }

    public static void CheckSchedule(AccountModel account)
    {
        string confirmText =
                "There are no location with schedule entries\n" +
                "Do you want to add to the schedule?\n" +
                "[1] Yes \n" +
                "[2] No\n";

        int confirmChoice = PresentationHelper.MenuLoop(confirmText, 1, 2);
        if (confirmChoice == 1)
        {
            CreateScheduleEntry.AddSchedule(account);
        }
        else if (confirmChoice == 2)
        {
            Console.WriteLine("\nSchedule creation cancelled\n");
            Menu.AdminMenu(account);
        }
    }

    public static void DisplaySchedule(LocationModel location)
    {
        List<ScheduleModel> schedules = ScheduleAccess.ScheduleByDateAndLocation(location);
        if (schedules.Count == 0)
        {
            PresentationHelper.Error("No schedules found");
            return;
        }
        var scheduleByDay = ScheduleLogic.GroupByDay(schedules);
        int dayIndex = SelectDay(scheduleByDay);

        if (dayIndex == -1)
            return;

        schedules = scheduleByDay[dayIndex].ToList();
        string text = $"Movies Playing on {scheduleByDay[dayIndex].Key:dd-MM-yy}";
        foreach (ScheduleModel schedule in schedules)
        {
            text += $"\nMovie: {schedule.Movie.Name}, Room: {schedule.Auditorium.Room}, Starting time: {schedule.StartTime:HH:mm}";
        }
        PresentationHelper.PrintAndEnter(text);
        Console.Clear();
    }
}