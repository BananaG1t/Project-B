namespace UnitTests;

[TestClass]
public class ValidDateTest
{
    [TestMethod]
    public void TestMethod1()
    {
        // Arrange
        string input = "15-11-2023-14-30";
        DateTime expectedDate = new DateTime(2023, 11, 15, 14, 30, 0);

        // Set up Console input to simulate user input
        using (var reader = new StringReader(input))
        {
            Console.SetIn(reader);

            // Act
            DateTime result = YourClass.ValidDate("Enter date:");

            // Assert
            Assert.AreEqual(expectedDate, result);
    }
}