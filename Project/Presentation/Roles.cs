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
            PresentationHelper.PrintAndEnter("There are no roles in the database");
            return;
        }

        RoleModel role = RoleLogic.GetAllRoles()[PresentationHelper.MenuLoop(RoleInfo.Item1, 1, RoleInfo.Item2) - 1];

        Tuple<string, int> allAccountInfo = AccountsLogic.GetAccountText();

        // makes sure the user doesn't go into an empty loop
        if (allAccountInfo.Item2 == 0)
        {
            PresentationHelper.PrintAndEnter("There are no accounts in the database");
            return;
        }

        AccountModel account = AccountsLogic.GetAllAccounts()[PresentationHelper.MenuLoop(allAccountInfo.Item1, 1, allAccountInfo.Item2) - 1];

        if (account.Id == 0)
        {
            Console.WriteLine("Cannot give the admin account a different role");
            return;
        }

        Tuple<string, int> locationInfo = LocationLogic.GetLocationInfo();

        // makes sure the user doesn't go into an empty loop
        if (locationInfo.Item2 == 0)
        {
            PresentationHelper.PrintAndEnter("There are no locations in the database");
            return;
        }

        int locationNumbChosen = PresentationHelper.MenuLoop(locationInfo.Item1, 1, locationInfo.Item2);

        LocationModel? locationModel = locationNumbChosen == 1 ? null : LocationLogic.GetAllLocations()[locationNumbChosen - 2];

        AssignedRoleModel? assignedRoleModel = RoleLogic.GetAssignedRoleByAccountId(account.Id);

        bool differentLocation = false;
        bool differentRole = false;

        if (assignedRoleModel != null)
        {
            RoleModel assignedrole = RoleAccess.GetById(assignedRoleModel.RoleId) ?? throw new Exception("Role not found");

            if (assignedRoleModel.LocationId == locationModel?.Id)
            {
                if (assignedrole.LevelAccess == role.LevelAccess)
                { PresentationHelper.PrintAndEnter($"That is the same role"); return; }

                Console.WriteLine("That account already has a role on that location");

                differentRole = true;
            }
            else
            {
                differentLocation = true;

                if (assignedrole.LevelAccess != role.LevelAccess)
                {

                    Console.WriteLine("That account already has a different role on another location");
                    differentRole = true;
                }
            }

            if (differentRole)
            {
                string text = $"which role do you want them to have\n[1] {role.Name}\n[2] {assignedrole.Name}";
                List<RoleModel> roles = [role, assignedrole];
                role = roles[PresentationHelper.MenuLoop(text, 1, 2) - 1];

                if (!RoleLogic.UpdateAssignedRolesByRole(assignedRoleModel, role))
                { PresentationHelper.PrintAndEnter("Cannot change the admin role\n"); }

                if (differentLocation)
                { RoleLogic.AssignRole(role.Id, account.Id, locationModel?.Id); }
            }
            else
            {
                if (differentLocation)
                { RoleLogic.AssignRole(role.Id, account.Id, locationModel?.Id); }
            }
        }

        if (!differentLocation && !differentRole)
        { RoleLogic.AssignRole(role.Id, account.Id, locationModel?.Id); }

    }

    public static bool CreateRole()
    {
        Console.Clear();

        string roleName = PresentationHelper.GetString("What is the name of the role? ", "role");
        int levelAccess = PresentationHelper.GetInt("What level access should the role have?");

        if (!RoleLogic.AddRole(roleName, levelAccess))
        {
            PresentationHelper.PrintAndEnter("That role name or level access already exists");
            return false;
        }

        Console.WriteLine("The functionality has been added to the database");
        return true;
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
                PresentationHelper.PrintAndEnter("That functionality or level already exists");
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
            PresentationHelper.PrintAndEnter("There are no assigned roles in the database");
            return;
        }

        AssignedRoleModel assignedRole = RoleLogic.GetAllAssignedRoles()[PresentationHelper.MenuLoop(assignedRoles.Item1, 1, assignedRoles.Item2) - 1];

        // makes sure the admin doesn't remove admin from himself otherwise i have to manually add it again
        // spoiler alert
        // it sucks
        if (assignedRole.AccountId == 0)
        {
            PresentationHelper.PrintAndEnter("You cannot remove your admin role");
            return;
        }

        RoleLogic.RemoveRole(assignedRole.Id);

        Console.Clear();
    }

    public static void DeleteRole()
    {
        Tuple<string, int> Roles = RoleLogic.GetRoleText();

        Console.Clear();

        if (Roles.Item2 == 0)
        {
            PresentationHelper.PrintAndEnter("There are no roles in the database");
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
            PresentationHelper.PrintAndEnter("You cannot remove the admin role");
            return;
        }

        RoleLogic.DeleteRole(role.Id);

        Console.Clear();
    }

    public static void DeleteFunctionalityRole()
    {
        Tuple<string, int> roleLevels = RoleLogic.GetRoleLevelText();

        Console.Clear();

        if (roleLevels.Item2 == 0)
        {
            PresentationHelper.PrintAndEnter("There are no assigned roles in the database");
            return;
        }

        RoleLevelModel roleLevel = RoleLogic.GetAllRoleLevels()[PresentationHelper.MenuLoop(roleLevels.Item1, 1, roleLevels.Item2) - 1];

        RoleLogic.DeleteFunctionalityRole(roleLevel.Id);

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