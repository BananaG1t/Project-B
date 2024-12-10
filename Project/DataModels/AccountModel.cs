public class AccountModel
{

    public int Id { get; set; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string? FullName { get; set; }

    public AccountModel(Int64 id, string email, string password, string? fullname)
    {
        Id = (int)id;
        EmailAddress = email;
        Password = password;
        FullName = fullname;
    }
    public AccountModel(string email, string password, string? fullname)
    {
        EmailAddress = email;
        Password = password;
        FullName = fullname;
        Id = AccountsAccess.Write(this);
    }
}
