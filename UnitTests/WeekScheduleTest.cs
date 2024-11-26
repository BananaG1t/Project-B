namespace UnitTests;

[TestClass]
public class ValidAccName
{
    [TestMethod]

    public class MovieLogicTests
    {
        public void IsAvailable_ReturnsTrue_WhenRoomIsAvailable()
        {
            int room = 1;
            DateTime startTime = new DateTime(2024, 11, 25, 10, 0, 0);
            DateTime endTime = startTime.AddHours(2); 

            bool result = MovieLogic.IsAvailable(room, startTime, endTime);

            Assert.True(result);
        }

        public void IsAvailable_ReturnsFalse_WhenRoomIsUnavailable()
        {
            int room = 2;
            DateTime startTime = new DateTime(2024, 11, 25, 14, 0, 0); 
            DateTime endTime = startTime.AddHours(2); 

            bool result = MovieLogic.IsAvailable(room, startTime, endTime);

            Assert.False(result);
        }
    }
}