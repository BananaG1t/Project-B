public static class SnacksLogic
{
    public static void Write(SnacksModel snack)
    {
        SnacksAccess.Write(snack);
    }

    public static void GetById(int id)
    {
        SnacksAccess.GetById(id);
    }

    public static void update(SnacksModel snack)
    {
        SnacksAccess.Update(snack);
    }
    public static void Delete(int id)
    {
        SnacksAccess.Delete(id);
    }

}