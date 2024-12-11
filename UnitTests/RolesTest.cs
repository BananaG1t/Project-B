namespace UnitTests;

[TestClass]
public class TestRoles
{
    [TestMethod]
    [DataRow("staff", true)]
    [DataRow("admin", true)]
    [DataRow("floor manager", true)]
    [DataRow("user", false)]
    [DataRow("watermeloen", false)]
    [DataRow("kip", false)]
    [DataRow("stevie wonder", false)]
    public void StaffRoleInDatabase(string roleName, bool expected)
    {
        RoleModel role = RoleAccess.GetByName(roleName);

        Assert.AreEqual(role != null, expected);
    }

    [TestMethod]
    [DataRow(true)]
    public void LocationIdCheck(bool expected)
    {
        List<AssignedRoleModel> assignedRoles = AssignedRoleAccess.GetAllAssignedRoles();
        foreach (AssignedRoleModel assignedRole in assignedRoles)
        {
            Assert.AreEqual(assignedRole.LocationId is long, expected);
        }
    }

    [TestMethod]
    [DataRow("Display income overview", "staff", true)]
    [DataRow("Display income overview", "floor manager", false)]
    [DataRow("Display income overview", "admin", false)]

    public void StaffRoleCgeck(string roleLevelName, string roleName, bool expected)
    {
        RoleModel role = RoleAccess.GetByName(roleName);
        RoleLevelModel roleLevel = RoleLevelAccess.GetByFunctionality(roleLevelName);
        Assert.AreEqual(role.LevelAccess < roleLevel.LevelNeeded, expected);
    }

    // [TestMethod]
    // [DataRow("kevin krull\n10", true)]
    // [DataRow("admin\n1", false)]
    // [DataRow("kevin krull 2\n255", false)]
    // [DataRow("staff\n2", false)]
    // [DataRow("kevin krull 3\n30", false)]
    // [DataRow("floor manager\n3", false)]
    // [DataRow("kevin krull 4\n50", false)]
    // public void CreateRollCheck(string input, bool expected)
    // {
    //     using (var reader = new StringReader(input))
    //     {
    //         Console.SetIn(reader);

    //         bool result = Roles.CreateRole();

    //         Assert.AreEqual(expected, result);
    //     }
    // }

    [TestMethod]
    [DataRow("admin", 100, false)]
    [DataRow("floor manager", 200, false)]
    [DataRow("staff", 300, false)]
    [DataRow("test1", 255, false)]
    [DataRow("test2", 50, false)]
    [DataRow("test3", 30, false)]
    [DataRow("test4", 1, true)]
    [DataRow("test5", 2, false)]
    [DataRow("test6", 3, true)]
    [DataRow("test7", 4, false)]
    [DataRow("test8", 5, true)]
    [DataRow("test9", 6, false)]
    [DataRow("test10", 10, true)]
    [DataRow("test11", 10, false)]
    [DataRow("test12", 20, true)]
    [DataRow("test13", 20, false)]
    [DataRow("test14", 40, true)]
    [DataRow("test15", 40, false)]

    public void TestAddRoleRole(string roleName, int LevelAccess, bool expected)
    {
        bool result = RoleLogic.AddRole(roleName, LevelAccess);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(0, 1, true)]
    [DataRow(0, 254, true)]
    [DataRow(0, 255, true)]
    [DataRow(0, 256, false)]
    [DataRow(2, 1, true)]
    [DataRow(2, 29, true)]
    [DataRow(2, 30, true)]
    [DataRow(2, 31, false)]
    [DataRow(3, 1, true)]
    [DataRow(3, 49, true)]
    [DataRow(3, 50, true)]
    [DataRow(3, 51, false)]
    public void TestAccessLevel(int accountId, int levelAccess, bool expected)
    {
        AccountModel acc = AccountsAccess.GetById(accountId);
        bool result = RoleLogic.HasAccess(acc, levelAccess);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow(0, "Manage Users", true)]
    [DataRow(0, "Add a movie", true)]
    [DataRow(0, "Add to the schedule", true)]
    [DataRow(0, "Display the schedule", true)]
    [DataRow(0, "Display income overview", true)]
    [DataRow(0, "Manage snacks", true)]
    [DataRow(0, "Manage Locations", true)]
    [DataRow(0, "Create coupon", true)]
    [DataRow(2, "Manage Users", false)]
    [DataRow(2, "Add a movie", false)]
    [DataRow(2, "Add to the schedule", true)]
    [DataRow(2, "Display the schedule", true)]
    [DataRow(2, "Display income overview", false)]
    [DataRow(2, "Manage snacks", true)]
    [DataRow(2, "Manage Locations", false)]
    [DataRow(2, "Create coupon", false)]
    [DataRow(3, "Manage Users", false)]
    [DataRow(3, "Add a movie", true)]
    [DataRow(3, "Add to the schedule", true)]
    [DataRow(3, "Display the schedule", true)]
    [DataRow(3, "Display income overview", true)]
    [DataRow(3, "Manage snacks", true)]
    [DataRow(3, "Manage Locations", false)]
    [DataRow(3, "Create coupon", true)]
    public void TestAccessFunctionality(int accountId, string functionality, bool expected)
    {
        AccountModel acc = AccountsAccess.GetById(accountId);
        bool result = RoleLogic.HasAccess(acc, functionality);
        Assert.AreEqual(expected, result);
    }
}
