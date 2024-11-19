namespace UnitTests;

[TestClass]
public class ValidDateTest
{
    [TestMethod]
    public void ValidDate()
    {
        string input = "15-12-2024-14-30";
        DateTime Date = new DateTime(2024, 12, 15, 14, 30, 0);

        // Set up Console input to simulate user input
        using (var reader = new StringReader(input))
        {
            Console.SetIn(reader);

            DateTime result = General.ValidDate("When do you want to show the movie? (dd-mm-yyy-hh-mm)");

            Assert.AreEqual(Date, result);
        }
    }
}