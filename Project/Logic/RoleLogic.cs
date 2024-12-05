public static class RoleLogic
{

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

    public static Tuple<string, int> GetRoleText()
    {
        List<RoleModel> roles = RoleAccess.GetAllRoles();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Name:{roles[i].Name} Level access: {roles[i].LevelAccess} ";
        }

        return new(text, roles.Count);
    }

    public static Tuple<string, int> GetRoleLevelText()
    {
        List<RoleLevelModel> roles = RoleLevelAccess.GetAllRoleLevels();

        string text = "";

        for (int i = 0; i < roles.Count; i++)
        {
            text += $"[{i + 1}] Name:{roles[i].Functionalty} Level access: {roles[i].LevelNeeded} ";
        }

        return new(text, roles.Count);
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
}