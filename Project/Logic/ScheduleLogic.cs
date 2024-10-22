public class ScheduleLogic
{

    public ScheduleLogic()
    {
        // Could do something here

    }

    public static bool IsAvailable(int room, DateTime startTime, TimeSpan length)
    {
        return ScheduleAccess.IsAvailable(room, startTime, startTime + length);
    }
}public class ScheduleLogic
{
    public int CalculateMaxIncome(int id)
    {
        if (id == 1)
        {
           return  
        }
    }
}