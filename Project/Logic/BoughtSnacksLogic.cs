public static class BoughtSnacksLogic
{
    public static void Write(BoughtSnacksModel snack)
    {
        BoughtSnacksAccess.Write(snack);
    }
    public static BoughtSnacksModel GetById(int id)
    {
        return BoughtSnacksAccess.GetById(id);
    }
    public static List<BoughtSnacksModel> GetAll()
    {
        return BoughtSnacksAccess.GetAll();
    }

    public static void Delete(int id)
    {
        BoughtSnacksAccess.Delete(id);
    }

}