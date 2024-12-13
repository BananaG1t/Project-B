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
            
            text += "\n";

            if (dayIndex != scheduleByDay.Count - 1)
            {
                valid.Add(1);
                text += "[1] Next\n";
            }
            if (dayIndex != 0)
            {
                valid.Add(2);
                text += "[2] Previous\n";
            }

            text += "[3] Back";
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