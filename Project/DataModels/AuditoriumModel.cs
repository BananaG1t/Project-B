public class AuditoriumModel
{
    public int Id { get; set; }
    public int Room { get; set; }

    public string? Type { get; set; }

    public int Total_seats { get; set; }
    private AuditoriumLogic Logic = new();

    public Dictionary<(int Row, int Collum), SeatModel> Seats = [];

    public AuditoriumModel(Int64 id, Int64 room, string type, Int64 total_seats)
    {
        Id = (int)id;
        Room = (int)room;
        Type = type;
        Total_seats = (int)total_seats;
        Seats = AuditoriumLogic.GetSeats(Id);
    }

    public AuditoriumModel(int room, string? type)
    {
        Room = room;
        Type = type;
        Total_seats = Room switch
        {
            1 => 150,
            2 => 300,
            3 => 500,
            _ => throw new NotImplementedException(),
        };
        Id = AuditoriumAcces.Write(this);
        Seats = AuditoriumLogic.CreateSeats(Id, Room);
    }
}



