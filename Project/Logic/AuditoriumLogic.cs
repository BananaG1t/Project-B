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
}
