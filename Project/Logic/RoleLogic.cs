public static class RoleLogic
{
    public static bool AssignRole(int roleId, int accountId, int? locationId)
    {
        if (!AssignedRoleAccess.IsAvailable(roleId, locationId, accountId)) return false;
        _ = new AssignedRoleModel(roleId, accountId, locationId);
        return true;
    }

    public static bool AddRole(string roleName, int LevelAccessMethod)
    {
        if (RoleAccess.GetByName(roleName) != null || RoleAccess.GetByRoleLevelAccess(LevelAccessMethod) != null)
        { return false; }

        _ = new RoleModel(roleName, LevelAccessMethod);
        return true;
    }

    public static bool AddRoleLevel(string functionalty, int roleLevel)
    {
        if (RoleLevelAccess.GetByLevel(roleLevel) != null || RoleLevelAccess.GetByFunctionality(functionalty) != null)
        { return false; }

        _ = new RoleLevelModel(functionalty, roleLevel);
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
            AssignedRoleModel? assignedRoleModel = AssignedRoleAccess.GetByRoleId(roleId);
            if (assignedRoleModel == null) { break; }
            AssignedRoleAccess.Delete(assignedRoleModel.Id);
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

        string text = string.Format("     {0,-15} | {1,-13} | {2,-10} | {3,-15}|\n", "Role name", "Level access", "Fullname", "Location name"); ;
        text += "===================================================================|\n";

        for (int index = 0; index < assignedRolesroles.Count; index++)
        {
            RoleModel? role = RoleAccess.GetById(assignedRolesroles[index].RoleId);

            if (role == null) { continue; }

            string roleName = role.Name;
            int roleLevel = role.LevelAccess;

            int? locationId = assignedRolesroles[index].LocationId;

            string LocationName = locationId != null ? (LocationLogic.GetById((int)locationId) ?? throw new Exception("Location not found")).Name : "All"; // GetById cant be null here cause of foreign key
            string? fullName = AccountsLogic.GetById(assignedRolesroles[index].AccountId)?.FullName;
            fullName ??= $"user{assignedRolesroles[index].AccountId}";
            int padding = (int)Math.Ceiling(Math.Ceiling(Math.Log10(assignedRolesroles.Count)) - Math.Log10(index + 1));

            // text += String.Format("[{0}] {1,-15} | {2,-13} | {3,-10} | {4,-15}|\n", (index + 1).ToString().PadRight((int)Math.Floor(Math.Log10(assignedRolesroles.Count) + 1)), roleName, roleLevel, fullName, LocationName);
            text += string.Format("[{0}]{1}{2,-15} | {3,-13} | {4,-10} | {5,-15}|\n", index + 1, "".PadLeft(padding), roleName, roleLevel, fullName, LocationName);
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

    public static AssignedRoleModel? GetAssignedRoleByAccountId(int id)
    { return AssignedRoleAccess.GetByAccountId(id); }

    public static RoleModel? GetRoleById(int id)
    { return RoleAccess.GetById(id); }

    public static RoleLevelModel? GetRoleLevelById(int id)
    { return RoleLevelAccess.GetById(id); }

    public static List<int> GetValidLevelAccess()
    {
        List<int> validLevels = [];

        foreach (RoleLevelModel role in RoleLevelAccess.GetAllRoleLevels())
        {
            validLevels.Add(role.LevelNeeded);
        }

        return validLevels;
    }


    public static bool HasAccess(AccountModel account, string functionaltyName)
    {
        AssignedRoleModel? assignedRoleModel = AssignedRoleAccess.GetByAccountId(account.Id);
        if (assignedRoleModel == null) { return false; }

        RoleLevelModel? roleLevelModel;
        while (true)
        {
            roleLevelModel = RoleLevelAccess.GetByFunctionality(functionaltyName);

            if (roleLevelModel == null)
            { Roles.CreateFunctionalityRole(functionaltyName); }
            else { break; }
        }

        RoleModel? roleModel = RoleAccess.GetById(assignedRoleModel.RoleId);
        if (roleModel == null) { return false; }

        if (roleModel.LevelAccess >= roleLevelModel.LevelNeeded)
            return true;

        return false;
    }

    public static bool HasAccess(AccountModel account, int levelNeeded)
    {
        AssignedRoleModel? assignedRoleModel = AssignedRoleAccess.GetByAccountId(account.Id);
        if (assignedRoleModel == null) { return false; }

        RoleModel? roleModel = RoleAccess.GetById(assignedRoleModel.RoleId);
        if (roleModel == null) { return false; }

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

        MenuOptions.Add("Logout");

        return MenuOptions;
    }

    public static RoleModel? GetRoleByAccountId(int accountId)
    {
        AssignedRoleModel? assignedRole = AssignedRoleAccess.GetByAccountId(accountId);
        if (assignedRole == null) { return null; }
        return RoleAccess.GetById(assignedRole.RoleId);
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

    public static bool UpdateAssignedRolesByRoleArrays(AssignedRoleModel assignedRole, RoleModel role)
    {
        AssignedRoleModel[] assignedRoles = AssignedRoleAccess.GetAllAssignedRoles(true);
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