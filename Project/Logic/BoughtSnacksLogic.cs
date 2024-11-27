public static class BoughtSnacksLogic
{
    public static void Write(Int64 account_id, Int64 reservationd_id, Int64 snack_id, int amount)
    {
        new BoughtSnacksModel(account_id, reservationd_id, snack_id, amount);
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