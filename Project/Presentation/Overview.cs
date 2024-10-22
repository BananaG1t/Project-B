static class Overview
{
    public static void MoneyOverview()
    {
        Console.WriteLine("Welcome to the income overview");
        Console.WriteLine("form which schedule would you like to see the income");
        int schedule = Convert.ToInt32(Console.ReadLine());

        ScheduleLogic schedules = new (); 
        Console.WriteLine($"income: {schedules.CalculateIncome(schedule)}");
        Console.WriteLine($"Total income: {schedules.CalculateMaxIncome(schedule)}");
        Console.WriteLine($"Amount of empty seats: {schedules.EmptySeats(schedule)}");    
    }
}