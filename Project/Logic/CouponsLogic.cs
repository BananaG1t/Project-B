public static class CouponsLogic
{
    public static void Write(string couponCode, DateTime expirationDate, string couponType, bool couponPercentage, float amount)
    {
        new CouponModel(couponCode, expirationDate, couponType, couponPercentage, amount);
    }
    public static CouponModel GetById(int id)
    {
        return CouponsAccess.GetById(id);
    }

    public static List<CouponModel> GetAll()
    {
        return CouponsAccess.GetAll();
    }

    public static void DeleteByCode(string code)
    {
        CouponsAccess.DeleteByCode(code);
    }

    public static string GenerateRandomCode(int length)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] code = new char[length];

        for (int i = 0; i < length; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        return new string(code);
    }

    public static float ValidFloat(string text, string errorText)
    {
        float price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();

            if (input.Contains(",")) { input = input.Replace(",", "."); }

            if (float.TryParse(input, out price) && price > 0)
            {
                valid = true;
            }
            else
            {
                PresentationHelper.Error(errorText);
            }
        }
        float roundedPrice = (float)Math.Round(price, 2);
        return roundedPrice;
    }
    public static float ValidFloatPercentage(string text, string errorText)
    {
        float price = 0;
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();

            if (input.Contains(",")) { input = input.Replace(",", "."); }

            if (float.TryParse(input, out price) && price > 0 && price <= 100)
            {
                valid = true;
            }
            else
            {
                PresentationHelper.Error(errorText);
            }
        }
        float roundedPrice = (float)Math.Round(price, 2);
        return roundedPrice;
    }

    public static double DiscountPrice(double price, CouponModel coupon)
    {
        if (coupon.CouponPercentage)
        {
            price -= price * coupon.Amount / 100;
        }
        else
        {
            price -= coupon.Amount;
            if (price < 0) price = 0;
        }
        return price;
    }

    public static double CalculateDiscount(double price, CouponModel coupon)
    {
        double newPrice;
        if (coupon.CouponPercentage)
        {
            newPrice = price * coupon.Amount / 100;
        }
        else
        {
            newPrice = price - coupon.Amount;
        }
        return newPrice;
    }
    public static List<CouponModel> GetAllByAccountId(int accountId, string couponType)
    {
        return CouponsAccess.GetAllByAccountId(accountId, couponType);
    }
}