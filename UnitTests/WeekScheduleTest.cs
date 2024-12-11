// namespace UnitTests;

// [TestClass]
// public class MovieLogicTests
// {
//     [TestMethod]
//     public void IsAvailable_ReturnsTrue_WhenRoomIsAvailable()
//     {
//         int room = 1;
//         DateTime startTime = new DateTime(2024, 11, 25, 10, 0, 0);
//         DateTime endTime = startTime.AddHours(2);
//         LocationModel location = new(111, "watermeloen");

//         Assert.IsTrue(ScheduleAccess.IsAvailable((room), startTime, endTime, (int)location.Id));
//     }

//     [TestMethod]
//     public void IsAvailable_ReturnsFalse_WhenRoomIsUnavailable()
//     {
//         int room = 2;
//         DateTime startTime = new DateTime(2024, 11, 25, 14, 0, 0);
//         DateTime endTime = startTime.AddHours(2);
//         LocationModel location = new((long)111, "watermeloen");

//         Assert.IsFalse(ScheduleAccess.IsAvailable(room, startTime, endTime, (int)location.Id));
//     }
// }
