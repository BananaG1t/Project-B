public static class Roles
{
    public static void RoleMenu()
    {
        Console.Clear();
        string text =
        $"Role Menu:\n" +
        "[1] Assign role to account\n" +
        "[2] Remove role from account\n" +
        "[3] Display all the assigned roles\n" +
        "[4] Create role\n" +
        "[5] Delete role\n" +
        "[6] Display all the roles\n" +
        // "[7] Create functionality role\n" +
        // "[8] Delete functionality role\n" +
        "[7] Display all the role levels\n" +
        "[8] go back to the menu";

        int menuChoices = 9;

        while (true)
        {
            int choice = PresentationHelper.MenuLoop(text, 1, menuChoices);

            if (choice == 1) { AssignRole(); }
            if (choice == 2) { RemoveRole(); }
            if (choice == 3) { DisplayRoles("Assigned Roles"); }
            if (choice == 4) { CreateRole(); }
            if (choice == 5) { DeleteRole(); }
            if (choice == 6) { DisplayRoles("Roles"); }
            // if (choice == 7) { CreateFunctionalityRole(); } // made this one obsolute but may be usefull later
            if (choice == 9) { DeleteFunctionalityRole(); } //  this one is secret use it if you want to
            if (choice == 7) { DisplayRoles("Role Levels"); }
            if (choice == 8) { break; }

            // made this just in case, if this happens we'll have a giant problem so thats why i want to spot this
            if (choice > menuChoices)
            {
                PresentationHelper.PrintInRed("Probleem");
                Console.WriteLine(choice);
            }

        }

        Console.Clear();
    }
    public static void AssignRole()
    {
        Tuple<string, int> RoleInfo = RoleLogic.GetRoleText();

        Console.Clear();

        // makes sure the user doesn't go into an empty loop
        if (RoleInfo.Item2 == 0)
        {
            PresentationHelper.PrintAndWait("There are no roles in the database");
            return;
        }

        RoleModel role = RoleLogic.GetAllRoles()[PresentationHelper.MenuLoop(RoleInfo.Item1, 1, RoleInfo.Item2) - 1];

        AccountsLogic acc = new();
        Tuple<string, int> allAccountInfo = acc.GetAccountText();

        // makes sure the user doesn't go into an empty loop
        if (allAccountInfo.Item2 == 0)
        {
            PresentationHelper.PrintAndWait("There are no accounts in the database");
            return;
        }

        AccountsLogic accountLogic = new();
        AccountModel account = accountLogic.GetAllAccounts()[PresentationHelper.MenuLoop(allAccountInfo.Item1, 1, allAccountInfo.Item2) - 1];

        Tuple<string, int> locationInfo = LocationLogic.GetLocationInfo();

        // makes sure the user doesn't go into an empty loop
        if (locationInfo.Item2 == 0)
        {
            PresentationHelper.PrintAndWait("There are no locations in the database");
            return;
        }

        LocationModel locationModel = LocationLogic.GetAllLocations()[PresentationHelper.MenuLoop(locationInfo.Item1, 1, locationInfo.Item2) - 1];

        AssignedRoleModel assignedRoleModel = RoleLogic.GetAssignedRoleByAccountId((int)account.Id);

        bool changeRole = false;

        if (assignedRoleModel != null && assignedRoleModel.LocationId == locationModel.Id)
        {
            Console.WriteLine("That account already has a role on that location");

            string text = "";
            if (RoleAccess.GetById((int)assignedRoleModel.RoleId).LevelAccess > role.LevelAccess)
            { text = $"Do you want to demote them?\n[1] Yes\n[2] No"; }
            if (RoleAccess.GetById((int)assignedRoleModel.RoleId).LevelAccess < role.LevelAccess)
            { text = $"Do you want to promote them?\n[1] Yes\n[2] No"; }
            if (RoleAccess.GetById((int)assignedRoleModel.RoleId).LevelAccess == role.LevelAccess)
            { text = $"That is the same role"; return; }

            changeRole = PresentationHelper.MenuLoop(text, 1, 2) == 1;
            if (!changeRole) { return; }
        }

        if (changeRole)
        { RoleLogic.RemoveRole((int)assignedRoleModel.Id); }

        if (RoleLogic.AssignRole((int)role.Id, account.Id, (int)locationModel.Id))
        { PresentationHelper.PrintAndWait("The role has been assigned"); }

    }

    public static void CreateRole()
    {
        Console.Clear();

        string roleName = PresentationHelper.GetString("What is the name of the role? ", "role");
        int levelAccess = PresentationHelper.GetInt("What level access should the role have?");

        if (RoleLogic.AddRole(roleName, levelAccess))
        {
            Console.WriteLine("The functionality has been added to the database");
            return;
        }

        PresentationHelper.PrintAndWait("That role name or level access already exists");
    }

    public static void CreateFunctionalityRole(string functionalityName)
    {
        Console.Clear();

        while (true)
        {
            PresentationHelper.PrintInRed($"{functionalityName} does not have an access level");
            int roleLevel = PresentationHelper.GetInt($"What level does {functionalityName} require?");

            if (!RoleLogic.AddRoleLevel(functionalityName, roleLevel))
            {
                PresentationHelper.PrintAndWait("That functionality or level already exists");
            }
            else
            {
                Console.WriteLine("The functionality has been added to the database");
                break;
            }
        }
    }

    public static void RemoveRole()
    {
        Tuple<string, int> assignedRoles = RoleLogic.GetAssignedRoleText();

        Console.Clear();

        // makes sure the user doesn't go into an empty loop
        if (assignedRoles.Item2 == 0)
        {
            PresentationHelper.PrintAndWait("There are no assigned roles in the database");
            return;
        }

        AssignedRoleModel assignedRole = RoleLogic.GetAllAssignedRoles()[PresentationHelper.MenuLoop(assignedRoles.Item1, 1, assignedRoles.Item2) - 1];

        // makes sure the admin doesn't remove admin from himself otherwise i have to manually add it again
        // spoiler alert
        // it sucks
        if (assignedRole.AccountId == 0)
        {
            PresentationHelper.PrintAndWait("You cannot remove your admin role");
            return;
        }

        RoleLogic.RemoveRole((int)assignedRole.Id);

        Console.Clear();
    }

    public static void DeleteRole()
    {
        Tuple<string, int> Roles = RoleLogic.GetRoleText();

        Console.Clear();

        if (Roles.Item2 == 0)
        {
            PresentationHelper.PrintAndWait("There are no roles in the database");
            return;
        }

        string text = "This will unassign roles to accounts, are you sure\n[1] yes\n[2] no";
        if (PresentationHelper.MenuLoop(text, 1, 2) == 2) { return; }

        RoleModel role = RoleLogic.GetAllRoles()[PresentationHelper.MenuLoop(Roles.Item1, 1, Roles.Item2) - 1];

        // makes sure the admin doesn't remove admin from himself otherwise i have to manually add it again
        // spoiler alert
        // it sucks
        if (role.LevelAccess == 255)
        {
            PresentationHelper.PrintAndWait("You cannot remove the admin role");
            return;
        }

        RoleLogic.DeleteRole((int)role.Id);

        Console.Clear();
    }

    public static void DeleteFunctionalityRole()
    {
        Tuple<string, int> roleLevels = RoleLogic.GetRoleLevelText();

        Console.Clear();

        if (roleLevels.Item2 == 0)
        {
            PresentationHelper.PrintAndWait("There are no assigned roles in the database");
            return;
        }

        RoleLevelModel roleLevel = RoleLogic.GetAllRoleLevels()[PresentationHelper.MenuLoop(roleLevels.Item1, 1, roleLevels.Item2) - 1];

        RoleLogic.DeleteFunctionalityRole((int)roleLevel.Id);

        Console.Clear();
    }

    public static void DisplayRoles(string displayType)
    {
        Tuple<string, int> displayInfo = new("", 0);

        if (displayType == "Assigned Roles")
        { displayInfo = RoleLogic.GetAssignedRoleText(); }
        if (displayType == "Roles")
        { displayInfo = RoleLogic.GetRoleText(); }
        if (displayType == "Role Levels")
        { displayInfo = RoleLogic.GetRoleLevelText(); }


        Console.Clear();

        Console.WriteLine(displayInfo.Item1);

        Console.WriteLine("[ENTER] Continue");
        Console.ReadLine();
        Console.Clear();
    }

}