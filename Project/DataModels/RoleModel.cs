public class RoleModel
{
    public Int64 Id { get; set; }
    public string RoleName { get; set; }
    public Int64 RoleLevelId { get; set; }

    public RoleModel(Int64 id, string Role_Name, Int64 Role_Level_ID)
    {
        Id = id;
        RoleName = Role_Name;
        RoleLevelId = Role_Level_ID;
    }

    public RoleModel(string Role_Name, Int64 Role_Level_ID)
    {
        RoleName = Role_Name;
        RoleLevelId = Role_Level_ID;
        Id = RoleAccess.Write(this);
    }
}