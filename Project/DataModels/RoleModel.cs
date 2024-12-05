public class RoleModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public Int64 LevelAccess { get; set; }

    public RoleModel(Int64 id, string name, Int64 level_Access)
    {
        Id = id;
        Name = name;
        LevelAccess = level_Access;
    }

    public RoleModel(string name, Int64 level_Access)
    {
        Name = name;
        LevelAccess = level_Access;
        Id = RoleAccess.Write(this);
    }
}