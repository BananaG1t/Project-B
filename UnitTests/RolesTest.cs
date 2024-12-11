namespace UnitTests;

[TestClass]
public class TestRoles
{
    [TestMethod]
    [DataRow("staff", true)]
    [DataRow("watermeloen", false)]
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

    [TestMethod]
    [DataRow("kevin krull\n10", true)]
    [DataRow("admin\n1", false)]
    [DataRow("kevin krull 2\n255", false)]
    [DataRow("staff\n2", false)]
    [DataRow("kevin krull 3\n30", false)]
    [DataRow("floor manager\n3", false)]
    [DataRow("kevin krull 4\n50", false)]
    public void CreateRollCheck(string input, bool expected)
    {
        using (var reader = new StringReader(input))
        {
            Console.SetIn(reader);

            bool result = Roles.CreateRole();

            Assert.AreEqual(expected, result);
        }
    }
}
