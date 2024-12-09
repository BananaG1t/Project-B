public class CouponModel
{
    Int64 Id { get; set; }
    int CouponCode { get; set; }
    DateTime ExpirationDate { get; set; }
    string CouponType { get; set; }
    Int64 CouponPercentage { get; set; }
    double Amount { get; set; }
    Int64 AccountId { get; set; }

    public CouponModel(Int64 id,int couponCode,DateTime expirationDate,string couponType,Int64 couponPercentage,double amount,Int64 Account_ID)
    {
        Id = id;
        CouponCode = couponCode;
        ExpirationDate = expirationDate;
        CouponType = couponType;
        CouponPercentage = couponPercentage;
        Amount = amount;
        AccountId = Account_ID;
    }

    public CouponModel(int couponCode,DateTime expirationDate,string couponType,Int64 couponPercentage,double amount,Int64 Account_ID)
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