public class CouponModel
{
    int Id { get; set; }
    int CouponCode { get; set; }
    DateTime ExpirationDate { get; set; }
    string CouponType { get; set; }
    bool CouponPercentage { get; set; }
    double Amount { get; set; }
    int AccountId { get; set; }

    public CouponModel(Int64 id,Int64 coupon_code,DateTime expiration_date,string coupon_type,Int64 coupon_percentage,double amount,Int64 Account_ID)
    {
        Id = (int)id;
        CouponCode = (int)coupon_code;
        ExpirationDate = expiration_date;
        CouponType = coupon_type;
        CouponPercentage = coupon_percentage == 1;
        this.Amount = amount;
        AccountId = (int)Account_ID;
    }

    public CouponModel(int couponCode,DateTime expirationDate,string couponType,bool couponPercentage,double amount,int Account_ID)
    {
        CouponCode = couponCode;
        ExpirationDate = expirationDate;
        CouponType = couponType;
        CouponPercentage = couponPercentage;
        Amount = amount;
        AccountId = Account_ID;
        Id = CouponsAccess.Write(this);
    }
}