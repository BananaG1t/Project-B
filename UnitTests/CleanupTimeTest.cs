namespace UnitTests;

[TestClass]
public class CleanupTimeTest
{
    [TestMethod]
    public void CleanupTimeTest()
    {
        // Makes a test schedule
        ScheduleModel TestSchedule = new ScheduleModel(new DateTime (3000, 12, 15, 12, 30, 00), 
        new MovieModel("Test", "Test", "Test", new TimeSpan(01, 30, 00), "Test", 99, 5.0),
        new AuditoriumModel(1, null));

        
    }
}