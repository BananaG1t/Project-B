namespace UnitTests;

[TestClass]
public class ValidAccName
{
    [TestMethod]
    public void ValidAccNameAndPassTest()
    {
        AccountsLogic logic = new AccountsLogic();

        string adminAccName = "A1";
        string AdminAccPass = "AP1";
        
        string UserAccName = "U1";
        string UserAccPass = "UP1";

        AccountModel AdminResult = logic.CheckLogin(adminAccName, AdminAccPass);
        AccountModel UserResult = logic.CheckLogin(UserAccName, UserAccPass);

        Assert.IsNotNull(AdminResult);
        Assert.IsNotNull(UserResult);
    }

    public void InvalidAccNameAndPassTest()
    {
        AccountsLogic logic = new AccountsLogic();

        string adminAccName = "A1";
        string AdminAccPass = "UP1";
        
        string UserAccName = "U1";
        string UserAccPass = "AP1";

        string InvalidAccName = "A2";

        AccountModel AdminResult = logic.CheckLogin(adminAccName, AdminAccPass);
        AccountModel UserResult = logic.CheckLogin(UserAccName, UserAccPass);
        AccountModel InvalidResult = logic.CheckLogin(InvalidAccName, AdminAccPass);

        Assert.IsNull(AdminResult);
        Assert.IsNull(UserResult);
        Assert.IsNull(InvalidResult);
    }

}