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
    public static void Delete(int id)
    {
        LocationAccess.Delete(id);
    }

}