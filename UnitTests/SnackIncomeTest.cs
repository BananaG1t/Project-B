namespace UnitTests;

[TestClass]
public class TestSnackIncomeOverview
{
    [TestMethod]
    [DataRow("weekly", 150.0, true)]   // Tests weekly snack income matches expected
    [DataRow("weekly", 200.0, false)]  // Tests weekly snack income does not match unexpected value
    [DataRow("daily", 50.0, true)]     // Tests daily snack income matches expected
    [DataRow("daily", 100.0, false)]   // Tests daily snack income does not match unexpected value
    [DataRow("movie", "Movie A", 70.0, true)]   // Tests snack income for a movie matches expected
    [DataRow("movie", "Movie B", 80.0, false)]  // Tests snack income for a movie does not match unexpected value
    [DataRow("reservation", 101, 30.0, true)]   // Tests snack income for a reservation matches expected
    [DataRow("reservation", 102, 50.0, false)]  // Tests snack income for a reservation does not match unexpected value
    public void TestSnackIncomeOverview1(string type, object identifier, double expectedIncome, bool expected)
    {
        double actualIncome = 0.0;

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
                string movieName = identifier.ToString();
                MovieModel movie = MovieLogic.GetAll().FirstOrDefault(m => m.Name == movieName);
                if (movie != null)
                {
                    actualIncome = SnacksLogic.CalculateIncomeByMovie(movie);
                }
                break;
            case "reservation":
                int reservationId = Convert.ToInt32(identifier);
                ReservationModel reservation = ReservationAcces.GetAllActive().FirstOrDefault(r => r.Id == reservationId);
                if (reservation != null)
                {
                    actualIncome = SnacksLogic.CalculateIncomeByReservation(reservation);
                }
                break;
        }

        Assert.AreEqual(actualIncome == expectedIncome, expected);
    }
}
