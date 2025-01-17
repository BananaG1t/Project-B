public class CouponModel
{
    public int Id { get; set; }
    public string CouponCode { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CouponType { get; set; }
    public bool CouponPercentage { get; set; }
    public double Amount { get; set; }

    public CouponModel(Int64 id, string coupon_code, string expiration_date, string coupon_type, Int64 coupon_percentage,Double amount)
    {
        string format = "yyyy-MM-dd HH:mm:ss";
        Id = (int)id;
        DateTime.TryParseExact(expiration_date, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        CouponCode = coupon_code;
        ExpirationDate = output;
        CouponType = coupon_type;
        CouponPercentage = coupon_percentage == 1;
        Amount = amount;
    }
    
    public CouponModel(string coupon_code,DateTime expiration_date,string coupon_type,bool coupon_percentage,double amount)
    {
        CouponCode = coupon_code;
        ExpirationDate = expiration_date;
        CouponType = coupon_type;
        CouponPercentage = coupon_percentage;
        Amount = amount;
        Id = CouponsAccess.Write(this);
    }

    public CouponModel(Int64 id, string coupon_code, string expiration_date, string coupon_type, string coupon_percentage, double amount)
    {
        // for dapper, when it tries to get a bool value from the database with no entry it returns a string
        string format = "yyyy-MM-dd HH:mm:ss";
        Id = (int)id;
        DateTime.TryParseExact(expiration_date, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        CouponCode = coupon_code;
        ExpirationDate = output;
        CouponType = coupon_type;
        CouponPercentage = false;
        Amount = amount;
    }
}