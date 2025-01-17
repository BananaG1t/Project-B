[TestClass]
public class CouponTests
{
    private static MockPresentationHelper MockPresentationHelper;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        MockPresentationHelper = new MockPresentationHelper();
    }

    [DataTestMethod]
    [DataRow(1, 1, 10, "31-12-2025", 1, "RANDOMCODE", 1, "Order", true)] // Valid input for Order with input code and valid date
    [DataRow(2, 1, 10, "31-12-2025", 1, "RANDOMCODE1", 1, "Seats", true)] // Valid input for Seat with input code and valid date
    [DataRow(3, 1, 10.5, "31-12-2025", 1, "RANDOMCODE2", 1, "Snacks", true)] // Valid input for Snack with input code and valid date
    [DataRow(1, 2, 10, "31-12-2025", 1, "RANDOMCODE3", 1, "snacks", false)] // Valid input for Order with input code and valid date check if snacks is false
    [DataRow(2, 2, 10, "31-12-2025", 1, "RANDOMCODE4", 1, "Order", false)] // Valid input for Seat with input code and valid date check if order is false
    [DataRow(3, 2, 10, "31-12-2025", 1, "RANDOMCODE5", 1, "Seats", false)] // Valid input for Snack with input code and valid date check if seats is false

    public void CreateCoupon_testinputs(int type, int input, double amount, string dateInput, int codeChoice, string codeInput, int readKey, string expectedCouponType, bool shouldSucceed)
    {
        // Arrange
        MockPresentationHelper.MenuLoopResults = new Queue<int>(new[] { type, input });

        // Redirect console input and output
        using (var inputReader = new StringReader($"{type}\n{input}\n{amount}\n{dateInput}\n{codeChoice}\n{codeInput}\n{readKey}"))
        using (var output = new StringWriter())
        {
            Console.SetIn(inputReader);
            Console.SetOut(output);
            try
            {
                // Act
                Coupon.IsTesting = true;
                PresentationHelper.IsTesting = true;
                Coupon.CreateCoupon();
            }
            catch (IOException ex) when (ex.Message.Contains("The handle is invalid"))
            {

            }

            // Assert
            var savedCoupon = CouponsAccess.GetByCode(codeInput);
            if (shouldSucceed)
            {
                Assert.IsNotNull(savedCoupon);
                Assert.AreEqual(expectedCouponType, savedCoupon.CouponType);
                Assert.AreEqual(codeInput, savedCoupon.CouponCode);
                Assert.AreEqual(amount, savedCoupon.Amount);
            }
            else
            {
                Assert.AreNotEqual(savedCoupon.CouponType, expectedCouponType);
            }
        }
        Coupon.IsTesting = false;
        PresentationHelper.IsTesting = false;        
    }

    [TestMethod]
    [DataRow("31-12-2025", "31-12-2025", true)] // Valid date
    [DataRow("invalid-date\n31-12-2025", "31-12-2025", true)] // Invalid date followed by valid date
    [DataRow("01-01-2000\n31-12-2025", "31-12-2025", true)] // Past date followed by valid date
    [DataRow("invalid-date\n01-01-2000\n31-12-2025", "31-12-2025", true)] // Invalid date followed by past date followed by valid date
    [DataRow("31-12-2025\ninvalid-date", "31-12-2025", true)] // Valid date followed by invalid date
    public void TestValideDate(string input, string expectedDateString, bool isValid)
    {
        // Arrange
        DateTime expectedDate = DateTime.ParseExact(expectedDateString, "dd-MM-yyyy", null);
        using (var inputReader = new StringReader(input))
        using (var output = new StringWriter())
        {
            Console.SetIn(inputReader);
            Console.SetOut(output);

            // Act
            DateTime result = DateTime.MinValue;
            try
            {
                PresentationHelper.IsTesting = true;
                result = PresentationHelper.ValidDate("Enter a valid date (dd-MM-yyyy):");
            }
            catch (FormatException)
            {
                // Expected exception for invalid date formats
            }

            // Assert
            if (isValid)
            {
                Assert.AreEqual(expectedDate, result);
            }
            else
            {
                Assert.AreEqual(DateTime.MinValue, result);
            }
        }
        PresentationHelper.IsTesting = false;
    }
}

public class MockPresentationHelper
{
    public Queue<int> MenuLoopResults { get; set; }
}