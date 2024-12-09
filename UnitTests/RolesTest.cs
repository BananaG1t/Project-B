using Xunit;
//Admin: Create roles

public class RolesTests
{
    [Fact]
    public void CreateRole_ShouldPromptForNameAndLevelAndAddRole()
    {
        // Simulate inputs for role name and level
        string roleName = "Admin";
        int levelAccess = 3;

        // Mock expected behavior of adding a role
        RoleLogic.AddRole = (name, level) =>
        {
            Assert.Equal(roleName, name);
            Assert.Equal(levelAccess, level);
            return true; // Simulate success
        };

        // Mock user input behavior
        PresentationHelper.GetString = (message, type) =>
        {
            Assert.Contains("What is the name of the role?", message);
            return roleName;
        };

        PresentationHelper.GetInt = (message) =>
        {
            Assert.Contains("What level access should the role have?", message);
            return levelAccess;
        };

        // Call the method to test
        Roles.CreateRole();
    }

    [Fact]
    public void CreateFunctionalityRole_ShouldPromptForFunctionalityAndLevel()
    {
        // Simulate inputs for functionality name and level
        string functionalityName = "ManageUsers";
        int roleLevel = 2;

        // Mock expected behavior of adding functionality
        RoleLogic.AddRoleLevel = (name, level) =>
        {
            Assert.Equal(functionalityName, name);
            Assert.Equal(roleLevel, level);
            return true; // Simulate success
        };

        // Mock user input behavior
        PresentationHelper.GetString = (message, type) =>
        {
            Assert.Contains("What is the name of the functionalty?", message);
            return functionalityName;
        };

        PresentationHelper.GetInt = (message) =>
        {
            Assert.Contains("What level does the functionalty require?", message);
            return roleLevel;
        };

        // Call the method to test
        Roles.CreateFunctionalityRole();
    }
}

/////_________________________________________________________________________________________________________________________________

//Admin: assign rolls to accounts
public class RolesTests
{
    [Fact]
    public void AssignRole_ShouldAssignRoleToAccount()
    {
        // Simulated database data for roles and accounts
        string roleMenu = "1. Admin\n2. User\n";
        int totalRoles = 2;
        string accountMenu = "1. JohnDoe\n2. JaneSmith\n";
        int totalAccounts = 2;

        // Simulate selected IDs
        int selectedRoleId = 1; // Admin
        int selectedAccountId = 2; // JaneSmith
        int locationId = 1; // Fixed or default location

        // Mock RoleLogic.GetRoleText to return roles
        RoleLogic.GetRoleText = () => new Tuple<string, int>(roleMenu, totalRoles);

        // Mock AccountsLogic.GetAccountText to return accounts
        var mockAccountLogic = new AccountsLogic();
        mockAccountLogic.GetAccountText = () => new Tuple<string, int>(accountMenu, totalAccounts);

        // Mock user input for selecting a role and an account
        PresentationHelper.MenuLoop = (menu, min, max) =>
        {
            if (menu.Contains("Admin")) return selectedRoleId; // Role selection
            if (menu.Contains("JohnDoe")) return selectedAccountId; // Account selection
            return 0; // Default
        };

        // Mock RoleLogic.AssignRole to verify it is called correctly
        RoleLogic.AssignRole = (roleId, accountId, locId) =>
        {
            Assert.Equal(selectedRoleId, roleId);
            Assert.Equal(selectedAccountId, accountId);
            Assert.Equal(locationId, locId);
            return true; // Simulate success
        };

        // Call the method to test
        Roles.AssignRole();
    }
}
///_________________________________________________________________________________________________________________________

///Staff: Staff role
public class StaffTests
{
    [Fact]
    public void CreateStaffAccount_ShouldCreateAccountWithPermissions()
    {
        // Simulated inputs for staff account creation
        string staffName = "JohnDoe";
        string cinemaLocation = "Downtown Cinema";
        string staffRole = "Scheduler";

        // Mock user inputs for staff details
        PresentationHelper.GetString = (message, type) =>
        {
            if (message.Contains("What is the staff name?")) return staffName;
            if (message.Contains("Which cinema does the staff work at?")) return cinemaLocation;
            if (message.Contains("What is the staff role?")) return staffRole;
            return "";
        };

        // Mock AccountLogic.CreateStaffAccount to verify the correct data is passed
        AccountLogic.CreateStaffAccount = (name, location, role) =>
        {
            Assert.Equal(staffName, name);
            Assert.Equal(cinemaLocation, location);
            Assert.Equal(staffRole, role);
            return true; // Simulate success
        };

        // Call the method to test
        Staff.CreateStaffAccount();

        // Verify success message
        PresentationHelper.PrintAndWait = (message) =>
        {
            Assert.Contains("Staff account created successfully", message);
        };
    }
}
