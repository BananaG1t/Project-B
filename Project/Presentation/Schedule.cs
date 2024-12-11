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
}