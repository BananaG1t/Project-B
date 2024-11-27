namespace UnitTests;

[TestClass]
public class MovieLogicTests
{
    [TestMethod]
    public void IsAvailable_ReturnsTrue_WhenRoomIsAvailable()
    {
        int room = 1;
        DateTime startTime = new DateTime(2024, 11, 25, 10, 0, 0);
        DateTime endTime = startTime.AddHours(2); 

        Assert.True(MovieLogic.IsAvailable(room, startTime, endTime));
    }

    [TestMethod]
    public void IsAvailable_ReturnsFalse_WhenRoomIsUnavailable()
    {
        int room = 2;
        DateTime startTime = new DateTime(2024, 11, 25, 14, 0, 0); 
        DateTime endTime = startTime.AddHours(2); 

        Assert.False(MovieLogic.IsAvailable(room, startTime, endTime));
    }
}
