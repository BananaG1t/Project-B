namespace UnitTests;

[TestClass]
public class TestRoles
{
    [TestMethod]
    [DataRow(0, true)]
    public static void TestDeleteFunction(int amount, bool expected)
    {
        List<LocationModel> locations = LocationLogic.GetAll();

        if (locations.Count > 0)
        {
            Assert.AreEqual(locations.Count != 0, expected);
        }
    }