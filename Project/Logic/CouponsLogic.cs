public static class CouponsLogic
{
    public static void Write(int couponCode, DateTime expirationDate, string couponType, Int64 couponPercentage, double amount, Int64 Account_ID)
    {
        new CouponModel(couponCode, expirationDate, couponType, couponPercentage, amount, Account_ID);
    }

}