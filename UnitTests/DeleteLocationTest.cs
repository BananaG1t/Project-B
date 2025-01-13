namespace UnitTests;

[TestClass]
public class TestDeleteLocationFunction
{
    [TestMethod]
    [DataRow(0, true)]
    public void TestDeleteFunction(int amount, bool expected)
    {
        // Get all current locations and deletes all of them
        List<LocationModel> locations = LocationLogic.GetAll();

        if (locations.Count > 0)
        {
            foreach (var loc in locations)
            {
                LocationLogic.Delete((int)loc.Id);
            }
            locations = LocationLogic.GetAll();
        }

        // Make sure all the data from Boughtsnacks, Reservation, Order, Schedule, Auditorium and Seats gets deleted too 
        List<ScheduleModel> schedules = ScheduleAccess.GetAll();
        List<AssignedRoleModel> assignedRoles = AssignedRoleAccess.GetAll();
        List<OrderModel> orders = OrderAccess.GetAll();
        List<ReservationModel> reservations = ReservationAcces.GetAll();
        List<BoughtSnacksModel> boughtSnacks = BoughtSnacksAccess.GetAll();
        List<AuditoriumModel> auditoriums = AuditoriumAcces.GetAll();
        List<SeatModel> seats = SeatsAccess.GetAll();

        Assert.AreEqual(locations.Count == amount, expected);
        Assert.AreEqual(schedules.Count == amount, expected);

        // Need to make sure that only the Admin roll stays
        Assert.AreEqual(assignedRoles.Count == 1, expected);
        AccountModel acc = AccountsAccess.GetById((int)assignedRoles[0].Id);
        Assert.AreEqual(acc.FullName == "Admin", expected);

        Assert.AreEqual(orders.Count == amount, expected);
        Assert.AreEqual(reservations.Count == amount, expected);
        Assert.AreEqual(boughtSnacks.Count == amount, expected);
        Assert.AreEqual(auditoriums.Count == amount, expected);
        Assert.AreEqual(seats.Count == amount, expected);
        
    }
}