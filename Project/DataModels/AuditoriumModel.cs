public class AuditoriumModel
{

    public Int64 Id { get; set; }
    public int LayoutID { get; set; }

    public string? Type { get; set; }

    public int Total_seats { get; set; }

    public bool Admin { get; set; }

    public AuditoriumModel(Int64 id, Int64 Layout_ID, string type, Int64 total_seats, Int64 admin)
    {
        Id = id;
        LayoutID = (int)Layout_ID;
        Type = type;
        Total_seats = (int)total_seats;
    }


}



