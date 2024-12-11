public class LocationModel
{

    public Int64 Id { get; set; }
    public string Name { get; set; }


    public LocationModel(Int64 id, string name)
    {
        Id = id;
        Name = name;
    }
    public LocationModel(string name)
    {
        Name = name;
        Id = LocationAccess.Write(this);
    }
}
