public static class RoleLogic
{
    public static bool AssignRole(int roleId, int accountId, int locationId)
    {
        AssignedRoleModel dbModel = AssignedRoleAccess.GetByLocationId(locationId);
        if (dbModel != null)
            if (dbModel.AccountId == accountId) { return false; }

        AssignedRoleModel assignedRoleModel = new(roleId, accountId, locationId);
        return true;
    }

    public static bool AddRole(string roleName, int LevelAccessMethod)
    {
        if (RoleAccess.GetByName(roleName) != null || RoleAccess.GetByRoleLevelAccess(LevelAccessMethod) != null)
        { return false; }

        RoleModel roleModel = new(roleName, LevelAccessMethod);
        return true;
    }

    public static bool AddRoleLevel(string functionalty, int roleLevel)
    {
        if (RoleLevelAccess.GetByLevel(roleLevel) != null || RoleLevelAccess.GetByFunctionality(functionalty) != null)
        { return false; }

        RoleLevelModel roleLevelModel = new(functionalty, roleLevel);
        return true;
    }

    public static void RemoveRole(int assignedRoleId)
    {
        AssignedRoleAccess.Delete(assignedRoleId);
    }

    public static void DeleteRole(int roleId)
    {
        while (true)
        {
            AssignedRoleModel assignedRoleModel = AssignedRoleAccess.GetByRoleId(roleId);
            if (assignedRoleModel == null) { break; }
            AssignedRoleAccess.Delete((int)assignedRoleModel.Id);
        }

        RoleAccess.Delete(roleId);
    }

    public static void DeleteFunctionalityRole(int roleLevelId)
    {
        RoleLevelAccess.Delete(roleLevelId);
    }

    public static Tuple<string, int> GetAssignedRoleText()
    {
        List<AssignedRoleModel> assignedRolesroles = GetAllAssignedRoles();

        AccountsLogic acc = new();

        string text = "";

        for (int index = 0; index < assignedRolesroles.Count; index++)
        {
            RoleModel role = RoleAccess.GetById((int)assignedRolesroles[index].RoleId);

            string roleName = role.Name;
            int roleLevel = (int)role.LevelAccess;
            string fullName = acc.GetById((int)assignedRolesroles[index].AccountId).FullName;
            string LocationName = LocationLogic.GetById((int)assignedRolesroles[index].LocationId).Name;
            if (LocationName == null) { LocationName = "All"; }

            text += $"[{index + 1}] Role name: {roleName}, " +
                    $"level access: {roleLevel}, " +
                    $"full name: {fullName}, " +
                    $"location name: {LocationName}\n";
        }

        return new(text, assignedRolesroles.Count);
    }

    public static Tuple<string, int> GetRoleText()
    {
        List<RoleModel> roles = GetAllRoles();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Name: {roles[i].Name}, Level access: {roles[i].LevelAccess}\n";
        }

        return new(text, roles.Count);
    }

    public static Tuple<string, int> GetRoleLevelText()
    {
        List<RoleLevelModel> roles = GetAllRoleLevels();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Functionalty: {roles[i].Functionalty}, Level needed: {roles[i].LevelNeeded}\n";
        }

        return new(text, roles.Count);
    }

    public static Tuple<string, int> GetFunctionalityText(List<string> functionalities)
    {
        string text = $"Choose a functionality\n";

        for (int i = 0; i < functionalities.Count; i++)
        {
            text += $"[{i + 1}] Functionalty: {functionalities[i]}\n";
        }

        return new(text, functionalities.Count);
    }

    public static List<AssignedRoleModel> GetAllAssignedRoles()
    {
        return AssignedRoleAccess.GetAllAssignedRoles();
    }

    public static List<RoleModel> GetAllRoles()
    {
        return RoleAccess.GetAllRoles();
    }

    public static List<RoleLevelModel> GetAllRoleLevels()
    {
        return RoleLevelAccess.GetAllRoleLevels();
    }

    public static AssignedRoleModel GetAssignedRoleByAccountId(int id)
    { return AssignedRoleAccess.GetByAccountId(id); }

    public static RoleModel GetRoleById(int id)
    { return RoleAccess.GetById(id); }

    public static RoleLevelModel GetRoleLevelById(int id)
    { return RoleLevelAccess.GetById(id); }

    public static List<int> GetValidLevelAccess()
    {
        List<int> validLevels = [];

        foreach (RoleLevelModel role in RoleLevelAccess.GetAllRoleLevels())
        {
            validLevels.Add((int)role.LevelNeeded);
        }

        return validLevels;
    }


    public static bool HasAccess(AccountModel account, string functionaltyName)
    {
        AssignedRoleModel assignedRoleModel = AssignedRoleAccess.GetByAccountId(account.Id);
        if (assignedRoleModel == null) { return false; }

        RoleLevelModel roleLevelModel = null;
        while (true)
        {
            roleLevelModel = RoleLevelAccess.GetByFunctionality(functionaltyName);

            if (roleLevelModel == null)
            { Roles.CreateFunctionalityRole(functionaltyName); }
            else { break; }
        }

        RoleModel roleModel = RoleAccess.GetById((int)assignedRoleModel.RoleId);

        if (roleModel.LevelAccess >= roleLevelModel.LevelNeeded)
            return true;

        return false;
    }

    public static bool HasAccess(AccountModel account, int levelNeeded)
    {
        AssignedRoleModel assignedRoleModel = AssignedRoleAccess.GetByAccountId(account.Id);
        if (assignedRoleModel == null) { return false; }

        RoleModel roleModel = RoleAccess.GetById((int)assignedRoleModel.RoleId);

        if (roleModel.LevelAccess >= levelNeeded)
            return true;

        return false;
    }

    public static List<string> GetMenuOptions(AccountModel account)
    {
        List<string> functionalities = Menu.functionalities;

        List<string> MenuOptions = [];

        for (int i = 0; i < functionalities.Count; i++)
        {
            if (HasAccess(account, functionalities[i]))
            {
                MenuOptions.Add($"{functionalities[i]}");
            }
        }

        MenuOptions.Add("Exit");

        return MenuOptions;
    }

    public static RoleModel GetRoleByAccountId(int accountId)
    {
        AssignedRoleModel assignedRole = AssignedRoleAccess.GetByAccountId(accountId);
        if (assignedRole == null) { return null; }
        return RoleAccess.GetById((int)assignedRole.RoleId);
    }

    public static bool UpdateAssignedRolesByRole(AssignedRoleModel assignedRole, RoleModel role)
    {
        List<AssignedRoleModel> assignedRoles = AssignedRoleAccess.GetAllAssignedRoles();
        foreach (AssignedRoleModel dbmodel in assignedRoles)
        {
            if (dbmodel.AccountId == assignedRole.AccountId)
            {
                if (dbmodel.RoleId == 0) { return false; }
                dbmodel.RoleId = role.Id;
                AssignedRoleAccess.Update(dbmodel);
            }
        }
        return true;
    }

    public static List<AssignedRoleModel> GetAllAssignedRolesByAccountId(int accountId)
    {
        return AssignedRoleAccess.GetAllByAccountId(accountId);
    }
}