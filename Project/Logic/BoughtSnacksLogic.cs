public static class BoughtSnacksLogic
{
    public static void Write(int reservation_id, int snack_id, int amount)
    {
        BoughtSnacksAccess.Write(reservation_id, snack_id, amount);
    }

    public static void Update(BoughtSnacksModel boughtSnacks)
    {
        BoughtSnacksAccess.Update(boughtSnacks);
    }

    public static BoughtSnacksModel GetById(int id)
    {
        return BoughtSnacksAccess.GetById(id);
    }
    public static List<BoughtSnacksModel> GetAll()
    {
        return BoughtSnacksAccess.GetAll();
    }

    public static List<BoughtSnacksModel> GetFromReservation(ReservationModel reservation)
    {
        return BoughtSnacksAccess.GetFromReservation(reservation);
    }

    public static void Delete(int id)
    {
        BoughtSnacksAccess.Delete(id);
    }

}