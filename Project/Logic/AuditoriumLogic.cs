//This class is not static so later on we can use inheritance and interfaces
public class AuditoriumLogic
{

    public AuditoriumLogic()
    {
        // Could do something here

    }

    public Dictionary<(int Row, int Collum), SeatModel> CreateSeats(Int64 id, int room_id)
    {
        Dictionary<(int Row, int Collum), SeatModel> Seats = [];
        List<AuditoriumLayoutModel> layout = AuditoriumLayoutAccess.GetById(room_id);
        foreach (AuditoriumLayoutModel seat in layout)
        {
            Seats[(seat.Row, seat.Collum)] = new SeatModel(id, seat.Row, seat.Collum, seat.Class, seat.Price);
        }

        return Seats;
    }

    public Dictionary<(int Row, int Collum), SeatModel> GetSeats(Int64 id)
    {
        Dictionary<(int Row, int Collum), SeatModel> Seats = [];
        List<SeatModel> seats = SeatsAccess.GetByRoom((int)id);
        foreach (SeatModel seat in seats)
        {
            Seats[(seat.Row, seat.Collum)] = seat;
        }

        return Seats;
    }

    public static void DisplaySeats(AuditoriumModel auditorium, int x, int y, int amountSelected)
    {
        int maxRow = auditorium.Seats.Keys.Max(k => k.Row);
        int maxCol = auditorium.Seats.Keys.Max(k => k.Collum);

        int cellWidth = 3; // Set a fixed width for cells
        string seperator = "------";

        // Print column headers with alignment
        Console.Clear();
        Console.Write(" 0 |  "); // Offset for row headers
        for (int col = 1; col <= maxCol; col++)
        {
            Console.Write($"{col.ToString().PadRight(cellWidth)}");
            seperator += new string('-', cellWidth);
        }
        Console.WriteLine();
        Console.WriteLine(seperator);
        SeatModel curSeat;

        // Print each row with cells aligned
        for (int row = 1; row <= maxRow; row++)
        {
            if (row != 1)
                Console.WriteLine();
            Console.ResetColor();
            Console.Write($"{row,2} |  "); // Row header with padding

            for (int col = 1; col <= maxCol; col++)
            {
                if (auditorium.Seats.ContainsKey((row, col)))
                {
                    curSeat = auditorium.Seats[(row, col)];

                    if (row == x && col - y >= 0 && col - y < amountSelected)
                        Console.ForegroundColor = ConsoleColor.White;

                    else
                    {
                        if (!curSeat.IsAvailable)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                        {
                            switch (curSeat.Class)
                            {
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;

                                case 2:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;

                                case 3:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                            }
                        }
                    }

                    if (curSeat.IsAvailable)
                        Console.Write("O".PadRight(cellWidth)); // Available seat with padding
                    else
                        Console.Write("X".PadRight(cellWidth)); // Available seat with padding
                }
                else
                {
                    Console.Write("".PadRight(cellWidth)); // Empty space with padding
                }
            }
        }
        Console.ResetColor();
        Console.WriteLine();
    }
}
