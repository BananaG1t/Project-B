public static class CouponsLogic
{
    public static void Write(int couponCode, DateTime expirationDate, string couponType, bool couponPercentage, double amount, int Account_ID)
    {
        new CouponModel(couponCode, expirationDate, couponType, couponPercentage, amount, Account_ID);
    }

}