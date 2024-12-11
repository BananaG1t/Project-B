public class CouponModel
{
    public int Id { get; set; }
    public int CouponCode { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CouponType { get; set; }
    public bool CouponPercentage { get; set; }
    public float Amount { get; set; }
    public int AccountId { get; set; }

    public CouponModel(Int64 id,Int64 coupon_code,DateTime expiration_date,string coupon_type,Int64 coupon_percentage,float amount,Int64 Account_ID)
    {
        Id = (int)id;
        CouponCode = (int)coupon_code;
        ExpirationDate = expiration_date;
        CouponType = coupon_type;
        CouponPercentage = coupon_percentage == 1;
        Amount = amount;
        AccountId = (int)Account_ID;
    }

    public CouponModel(int coupon_code,DateTime expiration_date,string coupon_type,bool coupon_percentage,float amount,int Account_ID)
    {
        CouponCode = coupon_code;
        ExpirationDate = expiration_date;
        CouponType = coupon_type;
        CouponPercentage = coupon_percentage;
        Amount = amount;
        AccountId = Account_ID;
        Id = CouponsAccess.Write(this);
    }
}