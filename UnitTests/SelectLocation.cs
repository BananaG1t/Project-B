namespace UnitTests;

[TestClass]
public class TestSelectLocationFunction
{
    [TestMethod]
    [DataRow("A", "", true)] 
    [DataRow("B", "", true)]
    [DataRow("C", "1", true)] // Valid input
    [DataRow("C", "10000", false)] // Invalid input (Out of range)
    [DataRow("C", "", false)] // Invalid input (Blank)
    [DataRow("C", "abc", false)] // Invalid input (Letters)
    [DataRow("C", "-1", false)] // Invalid input (Negative number)
    [DataRow("D", "1", true)] // Valid input
    [DataRow("D", "10000", false)] // nnvalid input (Out of range)
    [DataRow("D", "", false)] // Invalid input (Blank)
    [DataRow("D", "abc", false)] // Invalid input (Letters)
    [DataRow("D", "-1", false)] // Invalid input (Negative number)
    public void TestSelectFunction(string scenario, string choice, bool expected)
    {
        LocationMenu.IsTesting = true;
        PresentationHelper.IsTesting = true;

        // Get all current locations and deletes all of them
        List<LocationModel> locations = LocationLogic.GetAll();

        if (locations.Count > 0)
        {
            foreach (var loc in locations)
            {
                LocationLogic.Delete((int)loc.Id);
            }
        }
        
        // Scenario A is for when there is no locations
        if (scenario == "A")
        {
            LocationModel? location = LocationMenu.SelectLocation(new AccountModel("Test@.com", "Test", "Test"));

            Assert.AreEqual(location == null, expected);
        }

        // Scenario B is for when there is only locations with no schedules
        if (scenario == "B")
        {
            new LocationModel("Test");

            LocationModel? location = LocationMenu.SelectLocation(new AccountModel("Test@.com", "Test", "Test"));

            Assert.AreEqual(location == null, expected);
        }
        
        // Scenario C is for when there is only locations with schedules
        if (scenario == "C")
        {
            new LocationModel("Test");
            locations = LocationLogic.GetAll();

            ScheduleModel TestSchedule = new ScheduleModel(new DateTime (3000, 12, 15, 12, 00, 00), 
            new MovieModel ("Test", "Test", "Test", new TimeSpan(02, 00, 00), "Test", 18, 3),
            new AuditoriumModel(1, null), LocationLogic.GetById((int)locations[0].Id));

            using (var inputReader = new StringReader(choice))
            using (var outputWriter = new StringWriter())
            {
                Console.SetIn(inputReader);

                LocationModel? location = LocationMenu.SelectLocation(new AccountModel("Test@.com", "Test", "Test"));
                
                if (location == null)
                {
                    Assert.AreEqual(location == LocationLogic.GetById((int)locations[0].Id), expected);
                }
                else
                {
                Assert.AreEqual(location.Name == "Test", expected);   
                }
            }
        }

        // Scenario D is for when there is both locations with schedules and with no schedules
        if (scenario == "D")
        {
            new LocationModel("Test");
            new LocationModel("Test2");
            locations = LocationLogic.GetAll();

            ScheduleModel TestSchedule = new ScheduleModel(new DateTime (3000, 12, 15, 12, 00, 00), 
            new MovieModel ("Test", "Test", "Test", new TimeSpan(02, 00, 00), "Test", 18, 3),
            new AuditoriumModel(1, null), LocationLogic.GetById((int)locations[0].Id));

            using (var inputReader = new StringReader(choice))
            using (var outputWriter = new StringWriter())
            {
                Console.SetIn(inputReader);

                LocationModel? location = LocationMenu.SelectLocation(new AccountModel("Test@.com", "Test", "Test"));

                if (location == null)
                {
                    Assert.AreEqual(location == LocationLogic.GetById((int)locations[0].Id), expected);
                }
                else
                {
                Assert.AreEqual(location.Name == "Test", expected);   
                }   
            }
        }
        
        LocationMenu.IsTesting = false;
        PresentationHelper.IsTesting = false;

    }
}