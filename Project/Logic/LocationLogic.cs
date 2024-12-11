public static class LocationLogic
{
    public static void Add(string name)
    {
        new LocationModel(name);
    }

    public static LocationModel GetById(int id)
    {
        return LocationAccess.GetById(id);
    }

    public static List<LocationModel> GetAll()
    {
        return LocationAccess.GetAll();
    }

    public static List<string> GetAllNames()
    {
        return LocationAccess.GetAllNames();
    }

    public static List<LocationModel> GetAllLocationsWithNoSchedules()
    {
        return LocationAccess.GetAllLocationsWithNoSchedules();
    }

    public static void update(LocationModel location)
    {
        LocationAccess.Update(location);
    }
    public static void Delete(int locationId)
    {
        // AssignedRoleModel assignedRoleModel = AssignedRoleAccess.GetByLocationId(locationId);
        // OrderModel Order = OrderAccess.GetByLocationId(locationId);
        // ReservationModel reservation = ReservationAcces.GetFromOrder(locationId);
        // BoughtSnacksModel boughtSnacks = BoughtSnacksAccess.GetFromReservations(locationId);
        // ScheduleModel schedule = ScheduleAccess.GetByLocationId(locationId);

        // while (true)
        // {
        //     if (assignedRoleModel == null) { break; }
        //     AssignedRoleAccess.Delete((int)assignedRoleModel.Id);
        // }
        // while (true)
        // {
        //     if (boughtSnacks == null) { break; }
        //     BoughtSnacksAccess.Delete(boughtSnacks.Id);
        // }
        // while (true)
        // {
        //     if (reservation == null) { break; }
        //     ReservationAcces.Delete(reservation.Id);
        // }
        // while (true)
        // {
        //     if (Order == null) { break; }
        //     OrderAccess.Delete(Order.Id);
        // }
        // while (true)
        // {
        //     if (schedule == null) { break; }
        //     ScheduleAccess.Delete(schedule.Id);
        // }
        // LocationAccess.Delete(locationId);
    }

    public static Tuple<string, int> GetLocationInfo()
    {
        List<LocationModel> locations = LocationAccess.GetAll();

        string text = "";

        for (int i = 0; i < locations.Count; i++)
        {
            text += $"[{i + 1}] Name: {locations[i].Name}\n";
        }

        return new(text, locations.Count);
    }

    public static List<LocationModel> GetAllLocations()
    { return LocationAccess.GetAll(); }

}