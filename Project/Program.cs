class Program
{
    static void Main(string[] args)
    {
        var loginService = new LoginService();
        Console.WriteLine("Enter username:");
        string username = Console.ReadLine();
        Console.WriteLine("Enter password:");
        string password = Console.ReadLine();

        var user = loginService.Login(username, password);
        if (user != null)
        {
            if (user.IsAdmin)
            {
                var adminMenu = new AdminMenu();
                adminMenu.Show();
            }
            else
            {
                var userMenu = new UserMenu();
                userMenu.Show();
            }
        }
    }
}
