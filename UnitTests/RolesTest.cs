<<<<<<< Updated upstream
=======
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
}
>>>>>>> Stashed changes
