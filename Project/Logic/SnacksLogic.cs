public static class SnacksLogic
{
    public static void Add(string name, double price)
    {
        new SnacksModel(name, price);
    }

    public static SnacksModel GetById(int id)
    {
        return SnacksAccess.GetById(id);
    }

    public static List<SnacksModel> GetAll()
    {
        return SnacksAccess.GetAll();
    }

    public static void update(SnacksModel snack)
    {
        SnacksAccess.Update(snack);
    }
    public static void Delete(int id)
    {
        SnacksAccess.Delete(id);
    }

    private static List<SnacksModel> SnacksModel = new List<SnacksModel>
    {

    };

    public static double CalculateWeeklyIncome()
    {
        var oneWeekAgo = DateTime.Now.AddDays(-7);
        var weeklySales = SnacksModel.Where(sale => sale.Date >= oneWeekAgo).Sum(sale => sale.Amount);
        return weeklySales;
    }

    public static double CalculateDailyIncome()
    {
        var today = DateTime.Now.Date;
        var dailySales = SnacksModel.Where(sale => sale.Date.Date == today).Sum(sale => sale.Amount);
        return dailySales;
    }

    public static double CalculateIncomeByMovie(MovieModel movie)
    {
        var movieSales = SnacksModel.Where(sale => sale.MovieId == movie.Id).Sum(sale => sale.Amount);
        return movieSales;
    }

    public static double CalculateIncomeByReservation(BoughtSnacksModel boughtSnack)
    {
        double total = 0;
        List<BoughtSnacksModel> boughtSnacks = BoughtSnacksLogic.GetAllById(boughtSnack.ReservationId);
        if (boughtSnacks.Count == 0) return 0;
        foreach (BoughtSnacksModel currentSnack in boughtSnacks)
        {
            SnacksModel snack = GetById(currentSnack.SnackId);
            total += currentSnack.Amount * snack.Price;
        }
        return total;
    }


}