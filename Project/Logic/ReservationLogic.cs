public class ReservationLogic
{
    public ReservationLogic()
    {

    }

    public static int Write(ReservationModel reservation)
    {
        return ReservationAcces.Write(reservation);
    }

    public static void Update(ReservationModel reservation)
    {
        ReservationAcces.Update(reservation);
    }

    public static ReservationModel? GetById(int id)
    {
        return ReservationAcces.GetById(id);
    }

    public static List<ReservationModel> GetFromOrder(OrderModel order)
    {
        return ReservationAcces.GetFromOrder(order);
    }

    public static Int64 GetReservation_id(Int64 id)
    {
        return ReservationAcces.GetReservation_id(id);
    }

}