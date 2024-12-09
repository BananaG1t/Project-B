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
        List<AssignedRoleModel> assignedRolesroles = GetAllAssignedRoles();
        List<RoleModel> roles = GetAllRoles();

        AccountsLogic acc = new();

        string text = "";

        for (int index = 0; index < assignedRolesroles.Count; index++)
        {
            string roleName = roles[index].Name;
            int roleLevel = (int)roles[index].LevelAccess;
            string fullName = acc.GetById((int)assignedRolesroles[index].AccountId).FullName;
            string LocName = "";
            //string LocationName = LocationLogic.GetById(1).Name;

            text += $"[{index + 1}] Role name: {roleName}, " +
                    $"level access: {roleLevel}, " +
                    $"full name: {fullName}, " +
                    $"location name: {LocName}\n";
        }

        return new(text, assignedRolesroles.Count);
    }

    public static Tuple<string, int> GetRoleText()
    {
        List<RoleModel> roles = GetAllRoles();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Name:{roles[i].Name}, Level access: {roles[i].LevelAccess}\n";
        }

        return new(text, roles.Count);
    }

    public static Tuple<string, int> GetRoleLevelText()
    {
        List<RoleLevelModel> roles = GetAllRoleLevels();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Functionalty:{roles[i].Functionalty}, Level needed: {roles[i].LevelNeeded}\n";
        }

        return new(text, roles.Count);
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