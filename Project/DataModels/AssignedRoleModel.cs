public class AssignedRoleModel
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int AccountId { get; set; }
    public int? LocationId { get; set; }

    public AssignedRoleModel(Int64 id, Int64 Role, Int64 Account_Id, Int64 Location_Id)
    {
        Id = (int)id;
        RoleId = (int)Role;
        AccountId = (int)Account_Id;
        LocationId = (int)Location_Id;
    }

    public AssignedRoleModel(int roleId, int accountId, int? locationId)
    {
        RoleId = roleId;
        AccountId = accountId;
        LocationId = locationId;
        Id = AssignedRoleAccess.Write(this);
    }
}