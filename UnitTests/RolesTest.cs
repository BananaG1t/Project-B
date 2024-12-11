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
}
