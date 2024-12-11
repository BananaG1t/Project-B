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
        while (true)
        {
            AssignedRoleModel assignedRoleModel = AssignedRoleAccess.GetByLocationId(locationId);
            if (assignedRoleModel == null) { break; }
            AssignedRoleAccess.Delete((int)assignedRoleModel.Id);
        }
        while (true)
        {
            ScheduleModel schedule = ScheduleAccess.GetByLocationId(locationId);
            if (schedule == null) { break; }
            ScheduleAccess.Delete(schedule.Id);
        }
        LocationAccess.Delete(locationId);
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