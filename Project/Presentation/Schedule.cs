static class Schedule
{
    public static ScheduleModel SelectSchedule(LocationModel location)
    {
        List<ScheduleModel> schedules = ScheduleAccess.ScheduleByDateAndLocation(location);
        var scheduleByDay = ScheduleLogic.GroupByDay(schedules);
        int dayIndex = 0;

        int input = -1;
        while (input < 3 && input != 0)
        {
            Console.Clear();
            List<int> valid = [0];
            string text = $"Movies Playing on {scheduleByDay[dayIndex].Key:dd-MM-yy}";

            if (dayIndex != scheduleByDay.Count - 1)
            {
                valid.Add(1);
                text += "\n[1] Next day";
            }

            if (dayIndex != 0)
            {
                valid.Add(2);
                text += "\n[2] Previous day";
            }

            int cnt = 0;
            foreach (ScheduleModel schedule in scheduleByDay[dayIndex])
            {
                text += $"\n[{cnt + 3}] Movie: {schedule.Movie.Name}, Room: {schedule.Auditorium.Room}, Starting time: {schedule.StartTime}";
                valid.Add(cnt++ + 3);
            }



            text += "\n[0] Back";
            input = PresentationHelper.MenuLoop(text, valid);

            if (input == 0)
                return null;
            if (input == 1)
                dayIndex++;
            if (input == 2)
                dayIndex--;
        }

        return schedules[input - 3];
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
        List<ScheduleModel> schedules = ScheduleAccess.ScheduleByDateAndLocation(location);
        var scheduleByDay = ScheduleLogic.GroupByDay(schedules);
        int dayIndex = 0;

        int input = 0;

        while (input != 3)
        {
            Console.Clear();
            List<int> valid = [3];
            string text = $"Movies Playing on {scheduleByDay[dayIndex].Key:dd-MM-yy}";
            foreach (ScheduleModel schedule in scheduleByDay[dayIndex])
            {
                text += $"\nMovie: {schedule.Movie.Name}, Room: {schedule.Auditorium.Room}, Starting time: {schedule.StartTime:HH:mm}";
            }

            if (dayIndex != scheduleByDay.Count - 1)
            {
                valid.Add(1);
                text += "\n[1] Next";
            }
            if (dayIndex != 0)
            {
                valid.Add(2);
                text += "\n[2] Previous";
            }

            text += "\n[3] Back";
            input = PresentationHelper.MenuLoop(text, valid);

            if (input == 1)
                dayIndex++;
            if (input == 2)
                dayIndex--;
        }

        // Shows what movie are playing based on the date and time and location

        Console.Clear();
    }
}