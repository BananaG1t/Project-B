public static class CouponsLogic
{
    public static void Write(int couponCode, DateTime expirationDate, string couponType, bool couponPercentage, float amount, int Account_ID)
    {
        new CouponModel(couponCode, expirationDate, couponType, couponPercentage, amount, Account_ID);
    }
    public static CouponModel GetById(int id)
    {
        return CouponsAccess.GetById(id);
    }

    public static List<CouponModel> GetAllById(int id)
    {
        return CouponsAccess.GetAllById(id);
    }

    public static List<CouponModel> GetAll()
    {
        return CouponsAccess.GetAll();
    }

    public static float CalculateDiscount(float price,bool percentage)
    {
        float price -=
        if (percentage == true)
        {
            price 
        } 
    }
}