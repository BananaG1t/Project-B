public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

    public User(string username, string password, bool isAdmin)
    {
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
    }
}

public class LoginService
{
    private List<User> users = new List<User>
    {
        new User("admin", "adminpass", true),
        new User("user1", "user1pass", false)
    };

    public User Login(string username, string password)
    {
        var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user == null)
        {
            Console.WriteLine("Invalid username or password.");
            return null;
        }

        Console.WriteLine($"Welcome {user.Username}!");
        return user;
    }
}

public class UserMenu
{
    public void Show()
    {
        Console.WriteLine("User Menu:");
        Console.WriteLine("1. Option 1");
        Console.WriteLine("2. Option 2");
        Console.WriteLine("3. Exit");
    }
}

public class AdminMenu
{
    public void Show()
    {
        Console.WriteLine("Admin Menu:");
        Console.WriteLine("1. Manage Users (empty for now)");
        Console.WriteLine("2. System Settings (empty for now)");
        Console.WriteLine("3. Exit");
    }
}
