// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to this amazing program");
Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
Menu.Start();
