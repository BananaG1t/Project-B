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

    public override bool Equals(object obj)
    {
        if (obj is LocationModel other)
        {
            return Id == other.Id && Name == other.Name;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (Id + Name).GetHashCode();
    }

}
