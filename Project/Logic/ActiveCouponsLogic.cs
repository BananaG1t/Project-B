public static class ActiveCouponsLogic 
{
    public static void Write(int Account_ID, int Coupon_ID, int Uses)
    {
        ActiveCouponsAccess.Write(Account_ID, Coupon_ID, Uses);
    }
    public static ActiveCouponsModel GetById(int id)
    {
        return ActiveCouponsAccess.GetById(id);
    }
    public static List<ActiveCouponsModel> GetAll()
    {
        return ActiveCouponsAccess.GetAll();
    }

    public static void DeleteById(int id)
    {
        ActiveCouponsAccess.DeleteById(id);
    }

    public static bool CheckCoupon(int id) // added bool to check if coupon already exsits in database
    {
        ActiveCouponsModel coupon = ActiveCouponsAccess.GetById(id);
        if (coupon == null) return true;
        else return false;
    }

    public static int GetUses(int accountId, int couponId)
    {
        return ActiveCouponsAccess.GetUses(accountId, couponId);
    }

    public static List<ActiveCouponsModel> GetAllById(int accountId) 
    {
        return ActiveCouponsAccess.GetAllById(accountId);
    }

    public static int MaxUsesById(int accountId)
    {
        List<ActiveCouponsModel> coupons = GetAllById(accountId);
        int maxUses = 0;
        foreach (ActiveCouponsModel coupon in coupons) 
        {
            if (coupon.Uses > maxUses) 
            {
                maxUses = coupon.Uses;
            }
        }
        return maxUses;
    }
}