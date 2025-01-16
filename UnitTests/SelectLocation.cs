namespace UnitTests;

[TestClass]
public class TestSelectLocationFunction
{
    [TestMethod]
    [DataRow("A", "", true)]
    [DataRow("B", "", true)]
    [DataRow("C", "1", true)]
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
        
        if (scenario == "A")
        {
            LocationModel? location = LocationMenu.SelectLocation(new AccountModel("Test@.com", "Test", "Test"));

            Assert.AreEqual(location == null, expected);
        }

        if (scenario == "B")
        {
            new LocationModel("Test");

            LocationModel? location = LocationMenu.SelectLocation(new AccountModel("Test@.com", "Test", "Test"));

            Assert.AreEqual(location == null, expected);
        }

        // if (scenario == "C")
        // {
        //     new LocationModel("Test");

        //     using (var inputReader = new StringReader(choice))
        //     using (var outputWriter = new StringWriter())
        //     {
        //         Console.SetIn(inputReader);

        //         LocationModel location = LocationMenu.SelectLocation(new AccountModel("Test@.com", "Test", "Test"));

        //         Assert.AreEqual(location.Name == "Test", expected);   
        //     }
        // }
        
    }
}