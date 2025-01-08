namespace UnitTests
{
    [TestClass]
    public class CouponTests
    {
        [DataTestMethod]
        [DataRow(1, "VALIDCODE", 10, "VALIDCODE", true)] // Valid input code
        [DataRow(2, "RANDOMCODE", 10, "RANDOMCODE", true)] // Valid random code
        [DataRow(3, "", 0, "", false)] // Invalid input code
        [DataRow(2, "RANDOMCODE", -1, "", false)] // Invalid length for random code
        [DataRow(2, "RANDOMCODE", 0, "", false)] // Zero length for random code
        [DataRow(2, "RANDOMCODE", 5, "RANDOMCODE", true)] // Valid random code with different length
        [DataRow(1, "VALIDCODE", 10, "VALIDCODE", true, "01-01-2025")] // Valid input code with different expiration date
        [DataRow(2, "RANDOMCODE", 10, "RANDOMCODE", true, "31-12-2023")] // Valid random code with different expiration date
        public void CreateCoupon_ShouldHandleInputsCorrectly(int inputCode, string expectedCouponCode, int length, string generatedCode, bool shouldSucceed)
        {
            // Arrange
            var presentationHelper = new PresentationHelper();
            var couponsLogic = new CouponsLogic();

            // Mock the PresentationHelper and CouponsLogic methods
            presentationHelper.ValidDateFunc = (prompt) => new DateTime(2023, 12, 31);
            presentationHelper.MenuLoopFunc = (prompt, min, max) => inputCode;
            presentationHelper.GetIntFunc = (prompt) => length;
            couponsLogic.GenerateRandomCodeFunc = (len) => generatedCode;

            var coupon = new Coupon(presentationHelper, couponsLogic);

            // Act
            bool success = true;
            try
            {
                coupon.CreateCoupon();
            }
            catch (Exception)
            {
                success = false;
            }

            // Assert
            if (shouldSucceed)
            {
                Assert.AreEqual(expectedCouponCode, couponsLogic.LastWrittenCouponCode);
                Assert.AreEqual(new DateTime(2023, 12, 31), couponsLogic.LastWrittenExpirationDate);
            }
            Assert.AreEqual(shouldSucceed, success);
        }
    }

