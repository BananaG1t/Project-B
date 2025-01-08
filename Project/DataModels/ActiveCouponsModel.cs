public class ActiveCouponsModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int CouponId { get; set; }
    public int Uses { get; set; }

    public ActiveCouponsModel(Int64 id, Int64 Account_ID, Int64 Coupon_ID, Int64 Uses)
    {
        Id = (int)id;
        AccountId = (int)Account_ID;
        CouponId = (int)Coupon_ID;
        this.Uses = (int)Uses;
    }
    public ActiveCouponsModel(int Account_ID, int Coupon_ID, int Uses)
    {
        AccountId = Account_ID;
        CouponId = Coupon_ID;
        this.Uses = Uses;
        Id = ActiveCouponsAccess.Write(this);
    }

}