public static class RoleLogic
{
    public static bool AddRoleLevel(string functionalty, int roleLevel)
    {
        if (RoleLevelAccess.GetByLevel(roleLevel) != null || RoleLevelAccess.GetByFunctionality(functionalty) != null)
        { return false; }

        RoleLevelModel roleLevelModel = new(functionalty, roleLevel);
        return true;
    }

    public static bool AddRole(string roleName, int RolelevelIdMethod)
    {
        if (RoleAccess.GetByRoleName(roleName) != null)
        { return false; }

        RoleModel roleModel = new(roleName, RolelevelIdMethod);
        return true;
    }

    // public static Tuple<string, int> GetRoleText()
    // {
    //     List<RoleModel> roles = RoleAccess.GetAllRoles();

    //     string text = "";

    //     for (int i = 0; i < roles.Count; i++)
    //     {
    //         text += $"[{i + 1}] Name:{roles[i].RoleName} ";
    //     }
    // }
}