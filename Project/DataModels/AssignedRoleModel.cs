public class AssignedRoleModel
{
    public Int64 Id { get; set; }
    public Int64 RoleId { get; set; }
    public Int64 AccountId { get; set; }
    public Int64 LocationId { get; set; }

    public AssignedRoleModel(Int64 id, Int64 Role, Int64 Account_Id, Int64 Location_Id)
    {
        Id = id;
        RoleId = Role;
        AccountId = Account_Id;
        LocationId = Location_Id;
    }

    public AssignedRoleModel(int roleId, int accountId, int locationId)
    {
        RoleId = roleId;
        AccountId = accountId;
        LocationId = locationId;
        Id = AssignedRoleAccess.Write(this);
    }
}