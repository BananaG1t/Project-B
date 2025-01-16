//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        // Could do something here
    }

    public static AccountModel? GetById(int id)
    {
        return AccountsAccess.GetById(id);
    }

    public static AccountModel? GetByEmail(string email)
    {
        return AccountsAccess.GetByEmail(email);
    }

    public AccountModel? CheckLogin(string email, string password)
    {
        AccountModel? acc = AccountsAccess.GetByEmail(email);
        if (acc != null && acc.Password == password)
        {
            CurrentAccount = acc;
            return acc;
        }
        return null;
    }

    public bool CheckNewEmail(string email) // added bool to check if email already exsits in database
    {
        AccountModel? acc = AccountsAccess.GetByEmail(email);
        if (acc == null) return true;
        else return false;
    }

    public int Validinfo(string email, string password) // check if new email and password are valid 
    {
        if (!email.Contains('@') & !email.Contains('.'))
        {
            return 1;
        }
        else if (!CheckNewEmail(email))
        {
            return 2;
        }
        else if (password.Length < 1)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    public static List<AccountModel> GetAllAccounts()
    { return AccountsAccess.GetAllAccounts(); }

    public static Tuple<string, int> GetAccountText()
    {
        List<AccountModel> accounts = GetAllAccounts();

        string text = "";

        for (int i = 0; i < accounts.Count; i++)
        {
            text += $"[{i + 1}] {(accounts[i].FullName == null ? $"user{accounts[i].Id}" : accounts[i].FullName)} (id: {accounts[i].Id})\n";
        }

        return new(text, accounts.Count);
    }


}




