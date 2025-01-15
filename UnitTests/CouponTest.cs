public class CouponTests
{
    private static MockPresentationHelper MockPresentationHelper;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        MockPresentationHelper = new MockPresentationHelper();
    }

    [DataTestMethod]
    [DataRow(1, 1, 10, "RANDOMCODE", "Order", true)] // Valid input for Order with random code
    [DataRow(2, 2, 5, "RANDOMCODE", "Seats", true)] // Valid input for Seats with random code
    [DataRow(3, 1, 0, "", "Snacks", false)] // Invalid input for Snacks with invalid code length
    [DataRow(1, 2, 10, "RANDOMCODE", "Order", true)] // Valid input for Order with fixed amount
    [DataRow(2, 1, 5, "SHORT", "Seats", false)] // Invalid input for Seats with short random code
    [DataRow(3, 2, 15, "VALIDCODE12345", "Snacks", false)] // Valid input for Snacks with long random code
    [DataRow(1, 1, 0, "", "Order", true)] // Invalid input for Order with zero length code
    [DataRow(2, 2, 8, "VALID123", "Seats", true)] // Valid input for Seats with valid code
    public void CreateCoupon_ShouldHandleInputsCorrectly(int type, int input, int length, string generatedCode, string expectedCouponType, bool shouldSucceed)
    {
        // Arrange
        MockPresentationHelper.ValidDateResult = new DateTime(2025, 12, 31);
        MockPresentationHelper.MenuLoopResults = new Queue<int>(new[] { type, input });
        MockPresentationHelper.GetIntResult = length;

        // Redirect console input and output
        using (var inputReader = new StringReader($"{type}\n{input}\n{length}\n"))
        using (var output = new StringWriter())
        {
            Console.SetIn(inputReader);
            Console.SetOut(output);

            // Act
            Coupon.CreateCoupon();

            // Assert
            var savedCoupon = CouponsAccess.GetByCode(generatedCode);
            if (shouldSucceed)
            {
                Assert.IsNotNull(savedCoupon);
                Assert.AreEqual(expectedCouponType, savedCoupon.CouponType);
                Assert.AreEqual(generatedCode, savedCoupon.CouponCode);
            }
            else
            {
                Assert.IsNull(savedCoupon);
            }
        }
    }
}

public class MockPresentationHelper
{
    public DateTime ValidDateResult { get; set; }
    public Queue<int> MenuLoopResults { get; set; }
    public int GetIntResult { get; set; }
}