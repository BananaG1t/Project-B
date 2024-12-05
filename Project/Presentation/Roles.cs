using System.Data;

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
        "[7] Create functionality role\n" +
        "[8] Delete functionality role" +
        "[9] Display all the role levels";

        int choice = PresentationHelper.MenuLoop(text, 1, 9);

        if (choice == 1) { AssignRole(); }
        if (choice == 2) { RemoveRoll(); }
        if (choice == 3) { DisplayRoles(""); }
        if (choice == 4) { CreateRole(); }
        if (choice == 5) { DeleteRole(); }
        if (choice == 6) { DisplayRoles("Roles"); }
        if (choice == 7) { CreateFunctionalityRole(); }
        if (choice == 8) { DeleteFunctionalityRole(); }
        if (choice == 9) { DisplayRoles("Role Levels"); }

        if (choice > 9)
        {
            General.PrintInRed("Probleem");
            Console.WriteLine(choice);
        }
    }
    public static void AssignRole()
    {
    }

    public static void CreateRole()
    {
        string roleName = PresentationHelper.GetString("What is the name of the role? ", "role");
        int levelAccess = PresentationHelper.GetInt("What level access should the role have?");

        if (RoleLogic.AddRole(roleName, levelAccess))
        {
            Console.WriteLine("The functionality has been added to the database");
            return;
        }
        Console.WriteLine("That role name or level access already exists");
        Thread.Sleep(5000);
        Console.Clear();
    }

    public static void CreateFunctionalityRole()
    {
        string functionaltyName = PresentationHelper.GetString("What is the name of the functionalty? ", "functionalty");
        int roleLevel = PresentationHelper.GetInt("What level does the functionalty require? ");

        if (RoleLogic.AddRoleLevel(functionaltyName, roleLevel))
        {
            Console.WriteLine("The functionality has been added to the database");
            return;
        }
        Console.WriteLine("That functionality or level already exists");
        Thread.Sleep(5000);
        Console.Clear();
    }

    public static void RemoveRoll()
    {

    }

    public static void DeleteRole()
    {

    }

    public static void DeleteFunctionalityRole()
    {

    }

    public static void DisplayRoles(string displayType)
    {
        Tuple<string, int> displayInfo = new("", 0);

        if (displayType == "1")
        {
            // displayInfo = RoleLogic.GetRoleText();
        }
        if (displayType == "Roles")
        { displayInfo = RoleLogic.GetRoleText(); }
        if (displayType == "Role Levels")
        { displayInfo = RoleLogic.GetRoleLevelText(); }

        Console.WriteLine(displayInfo.Item1);

        Console.WriteLine("[ENTER] Continue");
    }

}