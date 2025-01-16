public class RoleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LevelAccess { get; set; }

    public RoleModel(Int64 id, string name, Int64 level_Access)
    {
        Id = (int)id;
        Name = name;
        LevelAccess = (int)level_Access;
    }

    public RoleModel(string name, int level_Access)
    {
        Name = name;
        LevelAccess = level_Access;
        Id = RoleAccess.Write(this);
    }
}