public class CouponModel
{
    int Id { get; set; }
    int CouponCode { get; set; }
    DateTime ExpirationDate { get; set; }
    string CouponType { get; set; }
    bool CouponPercentage { get; set; }
    double Amount { get; set; }
    int AccountId { get; set; }

    public CouponModel(Int64 id,Int64 Coupon_code,DateTime Expiration_date,string Coupon_type,Int64 Coupon_percentage,double Amount,Int64 Account_ID)
    {
        Id = (int)id;
        CouponCode = (int)Coupon_code;
        ExpirationDate = Expiration_date;
        CouponType = Coupon_type;
        CouponPercentage = Coupon_percentage == 1;
        this.Amount = Amount;
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