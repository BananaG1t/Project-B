using Moq;
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
        public void CreateCoupon_ShouldHandleInputsCorrectly(int inputCode, string expectedCouponCode, int length, string generatedCode, bool shouldSucceed)
        {
            // Arrange
            var mockPresentationHelper = new Mock<PresentationHelper>();
            var mockCouponsLogic = new Mock<CouponsLogic>();

            mockPresentationHelper.Setup(ph => ph.ValidDate(It.IsAny<string>())).Returns(new DateTime(2023, 12, 31));
            mockPresentationHelper.Setup(ph => ph.MenuLoop(It.IsAny<string>(), 1, 2)).Returns(inputCode);
            mockPresentationHelper.Setup(ph => ph.GetInt(It.IsAny<string>())).Returns(length);
            mockCouponsLogic.Setup(cl => cl.GenerateRandomCode(length)).Returns(generatedCode);

            var coupon = new Coupon(mockPresentationHelper.Object, mockCouponsLogic.Object);

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
                mockCouponsLogic.Verify(cl => cl.Write(expectedCouponCode, new DateTime(2023, 12, 31), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<decimal>()), Times.Once);
            }
            Assert.AreEqual(shouldSucceed, success);
        }
    }
}