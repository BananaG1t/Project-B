// namespace UnitTests;

// [TestClass]
// public class TestRoles
// {
//     [TestMethod]
//     [DataRow("staff", true)]
//     [DataRow("admin", true)]
//     [DataRow("floor manager", true)]
//     [DataRow("user", false)]
//     [DataRow("watermeloen", false)]
//     [DataRow("kip", false)]
//     [DataRow("stevie wonder", false)]
//     public void StaffRoleInDatabase(string roleName, bool expected)
//     {
//         RoleModel role = RoleAccess.GetByName(roleName);

//         Assert.AreEqual(role != null, expected);
//     }

//     [TestMethod]
//     [DataRow(true)]
//     public void LocationIdCheck(bool expected)
//     {
//         List<AssignedRoleModel> assignedRoles = AssignedRoleAccess.GetAllAssignedRoles();
//         foreach (AssignedRoleModel assignedRole in assignedRoles)
//         {
//             Assert.AreEqual(assignedRole.LocationId is long, expected);
//         }
//     }

//     [TestMethod]
//     [DataRow("Display income overview", "staff", true)]
//     [DataRow("Display income overview", "floor manager", false)]
//     [DataRow("Display income overview", "admin", false)]

//     public void StaffRoleCgeck(string roleLevelName, string roleName, bool expected)
//     {
//         RoleModel role = RoleAccess.GetByName(roleName);
//         RoleLevelModel roleLevel = RoleLevelAccess.GetByFunctionality(roleLevelName);
//         Assert.AreEqual(role.LevelAccess < roleLevel.LevelNeeded, expected);
//     }

//     // [TestMethod]
//     // [DataRow("kevin krull\n10", true)]
//     // [DataRow("admin\n1", false)]
//     // [DataRow("kevin krull 2\n255", false)]
//     // [DataRow("staff\n2", false)]
//     // [DataRow("kevin krull 3\n30", false)]
//     // [DataRow("floor manager\n3", false)]
//     // [DataRow("kevin krull 4\n50", false)]
//     // public void CreateRollCheck(string input, bool expected)
//     // {
//     //     using (var reader = new StringReader(input))
//     //     {
//     //         Console.SetIn(reader);

//     //         bool result = Roles.CreateRole();

//     //         Assert.AreEqual(expected, result);
//     //     }
//     // }

//     [TestMethod]
//     [DataRow("admin", 100, false)]          // tests if the admin name is in the db
//     [DataRow("floor manager", 200, false)]  // tests if the floor manager name is in the db
//     [DataRow("staff", 300, false)]          // tests if the staff name is in the db
//     [DataRow("test1", 255, false)]          // tests if the admin level is in the db
//     [DataRow("test2", 50, false)]           // tests if the floor manager level is in the db
//     [DataRow("test3", 30, false)]           // tests if the staff level is in the db
//     [DataRow("test4", 1, true)]             // tests double names
//     [DataRow("test4", 2, false)]            // tests double names
//     [DataRow("test5", 3, true)]             // tests double names
//     [DataRow("test5", 4, false)]            // tests double names
//     [DataRow("test6", 5, true)]             // tests double names
//     [DataRow("test6", 6, false)]            // tests double names
//     [DataRow("test10", 10, true)]           // tests double levels
//     [DataRow("test11", 10, false)]          // tests double levels
//     [DataRow("test12", 20, true)]           // tests double levels
//     [DataRow("test13", 20, false)]          // tests double levels
//     [DataRow("test14", 40, true)]           // tests double levels
//     [DataRow("test15", 40, false)]          // tests double levels

//     public void TestAddRoleRole(string roleName, int LevelAccess, bool expected)
//     {
//         bool result = RoleLogic.AddRole(roleName, LevelAccess);
//         Assert.AreEqual(expected, result);
//     }

//     [TestMethod]
//     [DataRow(0, 1, true)]       //checks if the admin has acces to everything it should
//     [DataRow(0, 255, true)]
//     [DataRow(0, 256, false)]
//     [DataRow(1, -1, false)]     //checks if a user has acces to everything it should
//     [DataRow(1, 0, false)]
//     [DataRow(1, 1, false)]
//     [DataRow(2, 1, true)]       //checks if staff has acces to everything it should
//     [DataRow(2, 29, true)]
//     [DataRow(2, 30, true)]
//     [DataRow(2, 31, false)]
//     [DataRow(3, 1, true)]       //checks if a floor manager has access to everything it should
//     [DataRow(3, 49, true)]
//     [DataRow(3, 50, true)]
//     [DataRow(3, 51, false)]
//     public void TestAccessLevel(int accountId, int levelAccess, bool expected)
//     {
//         AccountModel acc = AccountsAccess.GetById(accountId);
//         bool result = RoleLogic.HasAccess(acc, levelAccess);
//         Assert.AreEqual(expected, result);
//     }

//     [TestMethod]
//     [DataRow(0, "Manage Users", true)]              // checks if the admin has access to this function
//     [DataRow(0, "Add a movie", true)]               // checks if the admin has access to this function
//     [DataRow(0, "Add to the schedule", true)]       // checks if the admin has access to this function
//     [DataRow(0, "Display the schedule", true)]      // checks if the admin has access to this function
//     [DataRow(0, "Display income overview", true)]   // checks if the admin has access to this function
//     [DataRow(0, "Manage snacks", true)]             // checks if the admin has access to this function
//     [DataRow(0, "Manage Locations", true)]          // checks if the admin has access to this function
//     [DataRow(0, "Create coupon", true)]             // checks if the admin has access to this function
//     [DataRow(2, "Manage Users", false)]             // checks if the staff have access to this function
//     [DataRow(2, "Add a movie", false)]              // checks if the staff have access to this function
//     [DataRow(2, "Add to the schedule", true)]       // checks if the staff have access to this function
//     [DataRow(2, "Display the schedule", true)]      // checks if the staff have access to this function
//     [DataRow(2, "Display income overview", false)]  // checks if the staff have access to this function
//     [DataRow(2, "Manage snacks", true)]             // checks if the staff have access to this function
//     [DataRow(2, "Manage Locations", false)]         // checks if the staff have access to this function
//     [DataRow(2, "Create coupon", false)]            // checks if the staff have access to this function
//     [DataRow(3, "Manage Users", false)]             // checks if the floor managers have access to this function
//     [DataRow(3, "Add a movie", true)]               // checks if the floor managers have access to this function
//     [DataRow(3, "Add to the schedule", true)]       // checks if the floor managers have access to this function
//     [DataRow(3, "Display the schedule", true)]      // checks if the floor managers have access to this function
//     [DataRow(3, "Display income overview", true)]   // checks if the floor managers have access to this function
//     [DataRow(3, "Manage snacks", true)]             // checks if the floor managers have access to this function
//     [DataRow(3, "Manage Locations", false)]         // checks if the floor managers have access to this function
//     [DataRow(3, "Create coupon", true)]             // checks if the floor managers have access to this function
//     public void TestAccessFunctionality(int accountId, string functionality, bool expected)
//     {
//         AccountModel acc = AccountsAccess.GetById(accountId);
//         bool result = RoleLogic.HasAccess(acc, functionality);
//         Assert.AreEqual(expected, result);
//     }
// }
