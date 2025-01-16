public class AuditoriumLayoutModel
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Collum { get; set; }

    public double Price { get; set; }

    public int Class { get; set; }

    public bool IsAvailable { get; set; }

    public AuditoriumLayoutModel(Int64 room_id, Int64 row_num, Int64 col_num, Int64 seat_class, double price)
    {
        Id = (int)room_id;
        Row = (int)row_num;
        Collum = (int)col_num;
        Price = price;
        Class = (int)seat_class;
    }


}



