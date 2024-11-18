namespace UnitTests;

[TestClass]
public class CleanupTime
{
    [DataTestMethod]
    [DataRow("15-12-3000-14-30", true)]  // Test valid date
    [DataRow("15-12-3000-14-10", false)] // Test invalid date
    public void CleanupTimeTest(string date, bool expected)
    {
        // Makes a test schedule
        ScheduleModel TestSchedule = new ScheduleModel(new DateTime (3000, 12, 15, 12, 30, 00), 
        new MovieModel ("Test", "Test", "Test", new TimeSpan(01, 30, 00), "Test", 99, 5.0),
        new AuditoriumModel(1, null));

        DateTime ValidDate = General.ValidDate(date);

        bool result = CreateScheduleEntry.CleanupTime(ValidDate);

        Assert.AreEqual(expected, result);

        ScheduleAccess.Delete((int)TestSchedule.Id);

    }
}