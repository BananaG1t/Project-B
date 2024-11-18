namespace UnitTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestBarIsEmpty()
    {
        // start time is far in the future to make sure the bar will be empty on that time
        string startTime = "2126-11-14 18:30:00";
        string format = "yyyy-MM-dd HH:mm:ss";

        DateTime StartTime;
        DateTime.TryParseExact(startTime, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        StartTime = output;

        int MaxSeats = 40;

        //bar has 40 seats, so the list of valid seats should be 40 long
        Assert.IsTrue(BarReservationLogic.AvailableBarSeats(StartTime).Count() == MaxSeats);
    }

    [TestMethod]
    public void TestBarEnoughSeats()
    {
        // start time is far in the future to make sure the bar will be empty on that time
        string startTime = "2126-11-14 18:30:00";
        string format = "yyyy-MM-dd HH:mm:ss";

        DateTime StartTime;
        DateTime.TryParseExact(startTime, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        StartTime = output;

        List<int> availableBarSeats = BarReservationLogic.AvailableBarSeats(StartTime);

        int MaxSeats = 40;

        Assert.IsTrue(BarReservationLogic.BarAvailable(availableBarSeats, MaxSeats - 1));
        Assert.IsTrue(BarReservationLogic.BarAvailable(availableBarSeats, MaxSeats));
        Assert.IsFalse(BarReservationLogic.BarAvailable(availableBarSeats, MaxSeats + 1));
    }

    [TestMethod]
    public void TestBookBarSeat()
    {
        // start time is far in the future to make sure the bar will be empty on that time
        string startTime = "2126-11-14 18:30:00";
        string endTime = "2126-11-14 19:59:00";
        string format = "yyyy-MM-dd HH:mm:ss";

        DateTime StartTime;
        DateTime.TryParseExact(startTime, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        StartTime = output;

        AccountsLogic logic = new AccountsLogic();
        AccountModel user = logic.CheckLogin("U1", "UP1");
        ScheduleModel schedule = new(1, startTime, endTime, 1, 1);
        int MaxSeats = 40;
        int seatAmount = 1;

        // check if the bar is empty
        List<int> availableBarSeats = BarReservationLogic.AvailableBarSeats(StartTime);

        Assert.IsTrue(availableBarSeats.Count() == MaxSeats);
        // reserve a spot at the bar and check if it lets it
        Assert.IsTrue(BarReservationLogic.ReserveBarSeats(user, schedule, seatAmount, 1));
        // check if there is a spot less in the bar
        availableBarSeats = BarReservationLogic.AvailableBarSeats(StartTime);

        Assert.IsTrue(availableBarSeats.Count() == MaxSeats - seatAmount);
    }
}