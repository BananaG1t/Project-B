// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to this amazing program");
Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
List<SeatModel> seats = AuditoriumLayoutAcces.GetById(1);
foreach (SeatModel seat in seats)
{
    Console.WriteLine(seat.Id);
}
//Menu.Start();
