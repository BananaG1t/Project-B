namespace UnitTests;

[TestClass]
public class SnackReservationTest
{
    [DataTestMethod]
    [DataRow("Snicker", "Snicker")] 
    [DataRow("7up", "7up")] 
    [DataRow("88", "88")] 
    [DataRow("", null)] 
    public void ValidNameTest(string name, string? expected)
    {
        // Set up Console input to simulate user input
        using (var reader = new StringReader(name))
        {
            Console.SetIn(reader);

            string result = SnackReservation.ValidName();

            Assert.AreEqual(expected, result);
        }
    }

    [DataTestMethod]
    [DataRow("2", 2.0)] 
    [DataRow("1.5", 1.5)] 
    [DataRow("0.88", 0.88)] 
    [DataRow("0", 0.0)] 
    [DataRow("-1\n100\n", 100.0)]
    public void ValidDoubleTest(string price, double? expected)
    {
        // Set up Console input to simulate user input
        using (var reader = new StringReader(price))
        {
            Console.SetIn(reader);

            double result = SnackReservation.ValidDouble();

            Assert.AreEqual(expected, result);
        }
    }
}