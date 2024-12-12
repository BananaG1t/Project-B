public class CouponModel
{
    public int Id { get; set; }
    public int CouponCode { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CouponType { get; set; }
    public bool CouponPercentage { get; set; }
    public double Amount { get; set; }
    public int AccountId { get; set; }
    
    public CouponModel(Int64 id,Int64 coupon_code,string expiration_date,string coupon_type,Int64 coupon_percentage,Double amount,Int64 Account_ID)
    {
        string format = "yyyy-MM-dd";
        Id = (int)id;
        DateTime.TryParseExact(expiration_date, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        CouponCode = (int)coupon_code;
        ExpirationDate = output;
        CouponType = coupon_type;
        CouponPercentage = coupon_percentage == 1;
        Amount = amount;
        AccountId = (int)Account_ID;
    }
    
    public CouponModel(int coupon_code,DateTime expiration_date,string coupon_type,bool coupon_percentage,double amount,int Account_ID)
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