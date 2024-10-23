using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AuditoriumLogic
{

    public AuditoriumLogic()
    {
        // Could do something here

    }

    public Dictionary<(Int64 Id, int Row, int Collum), SeatModel> CreateSeats(Int64 id, int room_id)
    {
        Dictionary<(Int64 Id, int Row, int Collum), SeatModel> Seats = [];
        List<AuditoriumLayoutModel> layout = AuditoriumLayoutAccess.GetById(room_id);
        foreach (AuditoriumLayoutModel seat in layout)
        {
            Seats[(id, seat.Row, seat.Collum)] = new SeatModel(id, seat.Row, seat.Collum, seat.Class, seat.Price);
        }

        return Seats;
    }

    public Dictionary<(Int64 Id, int Row, int Collum), SeatModel> GetSeats(Int64 id)
    {
        Dictionary<(Int64 Id, int Row, int Collum), SeatModel> Seats = [];
        List<SeatModel> seats = SeatsAccess.GetByRoom((int)id);
        foreach (SeatModel seat in seats)
        {
            Seats[(id, seat.Row, seat.Collum)] = seat;
        }

        return Seats;
    }

    public static void DisplaySeats(AuditoriumModel auditorium)
    {
        int RowSize = AuditoriumLayoutAccess.GetRowSizeByRoomId(auditorium.Room);
        int ColumSize = AuditoriumLayoutAccess.GetColSizeByRoomId(auditorium.Room);

        Console.Write("      ");
        for (int k = 1; k < RowSize; k++)
        {
            Console.Write($"{k} ");
        }
        Console.WriteLine();
        for (int j = 1; j < ColumSize + 3; j++)
        {
            Console.WriteLine();
            Console.Write(j.ToString().PadLeft(3) + "    ");
            for (int k = 1; k < RowSize; k++)
            {
                if (auditorium.Seats.ContainsKey((auditorium.Id, j, k)))
                    if (auditorium.Seats[(auditorium.Id, j, k)].IsAvailable) Console.Write("O ");
                    else Console.Write("X ");
                else Console.Write("  ");
            }
        }
        Console.WriteLine();
    }
}
