static class Seats
{
    public static void DisplaySeats(AuditoriumModel auditorium, int x, int y, int amountSelected)
    {
        int maxRow = auditorium.Seats.Keys.Max(k => k.Row);
        int maxCol = auditorium.Seats.Keys.Max(k => k.Collum);
        List<double> totalPrice = [];

        int cellWidth = 3; // Set a fixed width for cells
        string seperator = "------";

        // Print column headers with alignment
        Console.Clear();
        Console.WriteLine("Arrow keys for directions, press enter to select, press backspace to go back");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Yellow = €15  ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Blue = €12.5  ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Green = €10");
        Console.ResetColor();

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
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        totalPrice.Add(curSeat.Price);
                    }

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
        var grouped = totalPrice.GroupBy(n => n)
                                    .Select(g => new { Number = g.Key, Count = g.Count() })
                                    .ToList();
        string equation = string.Join(" + ", grouped.Select(g => $"{g.Number} x {g.Count}"));
        Console.WriteLine($"\ntotal price: {equation} = {totalPrice.Sum()}");
    }
}