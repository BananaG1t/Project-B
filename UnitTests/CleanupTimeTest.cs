namespace UnitTests;

[TestClass]
public class CleanupTime
{
    [DataTestMethod]
    [DataRow("A", 0, "15-12-3000-14-30", "15/12/3000 14:30:00")] // Test valid date
    [DataRow("A", 0, "15-12-3000-14-21", "15/12/3000 14:21:00")] // Test valid date
    [DataRow("A", 0, "15-12-3000-14-20", "15/12/3000 14:20:00")] // Test valid date
    [DataRow("A", 0, "15-12-3000-14-19", "")] // Test invalid date
    [DataRow("A", 0, "15-12-3000-14-10", "")] // Test invalid date
    [DataRow("A", 0, "15-12-3000-14-00", "")] // Test invalid date
    [DataRow("B", 1, "15-12-3000-14-30", "15/12/3000 14:30:00")] // Test valid date at another location
    [DataRow("B", 1, "15-12-3000-14-21", "15/12/3000 14:21:00")] // Test valid date at another location
    [DataRow("B", 1, "15-12-3000-14-20", "15/12/3000 14:20:00")] // Test valid date at another location
    [DataRow("B", 1, "15-12-3000-14-19", "15/12/3000 14:19:00")] // Test valid date at another location
    [DataRow("B", 1, "15-12-3000-14-10", "15/12/3000 14:10:00")] // Test valid date at another location
    [DataRow("B", 1, "15-12-3000-14-00", "15/12/3000 14:00:00")] // Test valid date at another location
    public void CleanupTimeTest(string locationName, int locationId,string date, string expected)
    {
        CreateScheduleEntry.IsTesting = true;

        List<LocationModel> locations = LocationLogic.GetAll();

        if (locations.Count > 0)
        {
            foreach (var loc in locations)
            {
                LocationLogic.Delete((int)loc.Id);
            }
        }

        if (locationId == 0)
        {
            new LocationModel(locationName);
        }
        else
        {
            new LocationModel("Test");
            new LocationModel(locationName);
        }
        locations = LocationLogic.GetAll();

        ScheduleModel TestSchedule = new ScheduleModel(new DateTime (3000, 12, 15, 12, 00, 00), 
        new MovieModel ("Test", "Test", "Test", new TimeSpan(02, 00, 00), "Test", 18, 3),
        new AuditoriumModel(1, null), LocationLogic.GetById((int)locations[locationId].Id));

        using (var inputReader = new StringReader(date))
        using (var outputWriter = new StringWriter())
        {
            Console.SetIn(inputReader);
            DateTime? result = null;

            try
            {
                result = CreateScheduleEntry.SelectDate(1, new TimeSpan(02, 00, 00), (int)locations[0].Id);
            }
            catch (IOException ex) when (ex.Message.Contains("The handle is invalid"))
            {
                // Ignore the exception caused by a method loop in the test environment
            }
            
            string consoleOutput = outputWriter.ToString();

            if (result == null)
            {
                Assert.AreEqual(consoleOutput, expected);
            }
            else
            {
                Assert.AreEqual(result.ToString(), expected);
            } 
        }

        CreateScheduleEntry.IsTesting = false;
    }
}