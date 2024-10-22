// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to this amazing program");
Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
AuditoriumModel room = new(1, null);
MovieModel movie = new("testmove", "author", "desc", new TimeSpan(1, 20, 1), "genre", 16, "decent");
ScheduleModel scheduleEntry = new(DateTime.Now, movie, room);

//Menu.Start();
