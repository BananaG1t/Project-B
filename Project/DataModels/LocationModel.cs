public class LocationModel
{

    public int Id { get; set; }
    public string Name { get; set; }


    public LocationModel(Int64 id, string name)
    {
        Id = (int)id;
        Name = name;
    }
    public LocationModel(string name)
    {
        Name = name;
        Id = LocationAccess.Write(this);
    }
}
