namespace UnitTests;

[TestClass]
public class TestDeleteLocationFunction
{
    [TestMethod]
    [DataRow(0, true)]
    public static void TestDeleteFunction(int amount, bool expected)
    {
        List<LocationModel> locations = LocationLogic.GetAll();

        if (locations.Count > 0)
        {
            foreach (var loc in locations)
            {
                LocationLogic.Delete((int)loc.Id);
            }
            locations = LocationLogic.GetAll();
            Assert.AreEqual(locations.Count == amount, expected);
        }
    }
}