namespace UnitTests;

[TestClass]
public class CleanupTime
{
    [DataTestMethod]
    [DataRow("15-12-3000-14-30", true)]  // Test valid date
    [DataRow("15-12-3000-14-10", false)] // Test invalid date
    public void CleanupTimeTest(string date, string expected)
    {
        List<MovieModel> Movies = MovieLogic.GetAll();
        if (Movies.Count == 0)
        {
            new MovieModel("Test", "Test", "Test", new TimeSpan(02, 00, 00), "Test", 18, 3);
        }

        List<LocationModel> locations = LocationLogic.GetAll();

        if (locations.Count > 0)
        {
            foreach (var loc in locations)
            {
                LocationLogic.Delete((int)loc.Id);
            }
        }

        new LocationModel("Test");
        locations = LocationLogic.GetAll();

        ScheduleModel TestSchedule = new ScheduleModel(new DateTime (3000, 12, 15, 12, 00, 00), 
        new MovieModel ("Test", "Test", "Test", new TimeSpan(02, 00, 00), "Test", 18, 3),
        new AuditoriumModel(1, null), new LocationModel("Test"));

        using (var inputReader = new StringReader(date))
        {
            Console.SetIn(inputReader);

            // Call the main method to test
            DateTime result = CreateScheduleEntry.SelectDate(1, new TimeSpan(02, 00, 00), LocationLogic.GetById(locations[0]));
            
            Assert.AreEqual(expected, result);
        }

    }
}