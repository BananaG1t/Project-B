public static class RoleLogic
{
    public static bool AssignRole(int roleId, int accountId, int locationId)
    {
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
        List<AssignedRoleModel> assignedRolesroles = AssignedRoleAccess.GetAllAssignedRoles();
        List<RoleModel> roles = RoleAccess.GetAllRoles();

        AccountsLogic acc = new();

        string text = "";

        for (int i = 0; i < assignedRolesroles.Count; i++)
        {
            string roleName = roles[i].Name;
            int roleLevel = (int)roles[i].LevelAccess;
            string fullName = acc.GetById((int)assignedRolesroles[i].AccountId).FullName;
            string LocName = "";
            //string LocationName = LocationLogic.GetById(1).Name;

            text += $"[{i + 1}] {roleName} {roleLevel} {fullName} {LocName}\n";
        }

        return new(text, assignedRolesroles.Count);
    }

    public static Tuple<string, int> GetRoleText()
    {
        List<RoleModel> roles = RoleAccess.GetAllRoles();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Name:{roles[i].Name}, Level access: {roles[i].LevelAccess}\n";
        }

        return new(text, roles.Count);
    }

    public static Tuple<string, int> GetRoleLevelText()
    {
        List<RoleLevelModel> roles = RoleLevelAccess.GetAllRoleLevels();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Functionalty:{roles[i].Functionalty}, Level needed: {roles[i].LevelNeeded}\n";
        }

        return new(text, roles.Count);
    }

    public static List<int> GetRoleIds()
    {
        List<RoleModel> roles = RoleAccess.GetAllRoles();

        List<int> ids = [];

        foreach (RoleModel roleLevelModel in roles)
            ids.Add((int)roleLevelModel.Id);

        return ids;
    }

    public static List<int> GetRoleLevelIds()
    {
        List<RoleModel> roles = RoleAccess.GetAllRoles();

        List<int> ids = [];

        foreach (RoleModel roleModel in roles)
            ids.Add((int)roleModel.Id);

        return ids;
    }

    public static List<int> GetValidLevelAccess()
    {
        List<int> validLevels = [];

        foreach (RoleLevelModel role in RoleLevelAccess.GetAllRoleLevels())
        {
            validLevels.Add((int)role.LevelNeeded);
        }

        return validLevels;
    }

    // public static string GetLocations()
    // {
    //     List<LocationModel> locations = LocationAcces.GetAllLocations();

    //     string text = "";

    //     for (int i = 0; i < locations.Count; i++)
    //     {
    //         text += $"[{i + 1}] Name:{locations[i].Name}\n";
    //     }

    //     return new(text, locations.Count);
    // }
}