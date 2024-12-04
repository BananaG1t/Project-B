public class CouponModel
{
    Int64 Id { get; set; }
    int CouponCode { get; set; }
    string ExpirationDate { get; set; }
    string CouponType { get; set; }
    bool CouponPercentage { get; set; }
    int Amount { get; set; }
    Int64 AccountId { get; set; }

    public CouponModel(Int64 id,int couponCode,string expirationDate,string couponType,bool couponPercentage,int amount,Int64 Account_ID)
    {
        Id = id;
        CouponCode = couponCode;
        ExpirationDate = expirationDate;
        CouponType = couponType;
        CouponPercentage = couponPercentage;
        Amount = amount;
        AccountId = Account_ID;
    }

    public CouponModel(int couponCode,string expirationDate,string couponType,bool couponPercentage,int amount,Int64 Account_ID)
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