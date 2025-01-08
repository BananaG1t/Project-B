namespace UnitTests;

[TestClass]
public class TestSnackIncomeOverview
{
    [TestMethod]
    [DataRow("weekly", 395.0, true)]   // Tests weekly snack income matches expected
    [DataRow("weekly", 200.0, false)]  // Tests weekly snack income does not match unexpected value
    [DataRow("daily", 695.0, true)]     // Tests daily snack income matches expected
    [DataRow("daily", 100.0, false)]   // Tests daily snack income does not match unexpected value
    [DataRow("movie", 150.0, true)]   // Tests snack income for a movie matches expected
    [DataRow("movie", 80.0, false)]  // Tests snack income for a movie does not match unexpected value
    [DataRow("reservation", 150.0, true)]   // Tests snack income for a reservation matches expected
    [DataRow("reservation", 50.0, false)]  // Tests snack income for a reservation does not match unexpected value
    public void TestSnackIncomeOverview1(string type, double expectedIncome, bool expected)
    {
        double actualIncome = 0.0;
        TimeSpan timeSpan = new TimeSpan(1, 0, 0);
        MovieModel movie = new("name", "director", "shrek is good", timeSpan, "horror", 16, 4.0);
        LocationModel location = new("Schoonhoven");
        AuditoriumModel auditorium = AuditoriumAcces.GetById(1);
        ScheduleModel schedule = new(DateTime.Now, movie, auditorium, location);
        AccountModel account = AccountsAccess.GetById(1);
        OrderModel order = new(account.Id, schedule.Id, 1, true);
        ReservationModel reservation = new(order.Id, 1, 1);
        reservation.Id = ReservationAcces.Write(new(order.Id, 1, 1));
        SnacksModel snacks1 = new("mars", 10);
        SnacksModel snacks2 = new("twix", 5.5);
        SnacksModel snacks3 = new("bami hapje", 4.5);
        BoughtSnacksModel boughtsnacks1 = new(reservation.Id, snacks1.Id, 8);
        BoughtSnacksModel boughtsnacks2 = new(reservation.Id, snacks2.Id, 7);
        BoughtSnacksModel boughtsnacks3 = new(reservation.Id, snacks3.Id, 7);

        // Call the appropriate method based on type
        switch (type)
        {
            case "weekly":
                actualIncome = SnacksLogic.CalculateWeeklyIncome();
                break;
            case "daily":
                actualIncome = SnacksLogic.CalculateDailyIncome();
                break;
            case "movie":
                if (movie != null)
                {
                    actualIncome = SnacksLogic.CalculateIncomeByMovie(movie);
                }
                break;
            case "reservation":
                {
                    actualIncome = SnacksLogic.CalculateIncomeByReservation(boughtsnacks1);

                }
                break;
        }
        Assert.AreEqual(actualIncome == expectedIncome, expected);
    }
}
