public class SeatModel
{

    public Int64 Id { get; set; }
    public Int64 AuditoriumId { get; set; }
    public int Row { get; set; }
    public int Collum { get; set; }

    public double Price { get; set; }

    public int Class { get; set; }

    public bool IsAvailable { get; set; }

    public SeatModel(Int64 room_id, Int64 row_num, Int64 col_num, Int64 seat_class, double price)
    {
        AuditoriumId = room_id;
        Row = (int)row_num;
        Collum = (int)col_num;
        Price = price;
        Class = (int)seat_class;
        IsAvailable = true;
        Id = SeatsAccess.Write(this);
    }

    public SeatModel(Int64 id, Int64 Auditorium_ID, Int64 row, Int64 collum, double price, Int64 type, Int64 isAvailable)
    {
        Id = id;
        AuditoriumId = Auditorium_ID;
        Row = (int)row;
        Collum = (int)collum;
        Price = price;
        Class = (int)type;
        IsAvailable = isAvailable == 1;
    }
    
    // This only for when the Seat table is empty (isAvailable column returns a string as default value)
    public SeatModel(Int64 id, Int64 Auditorium_ID, Int64 row, Int64 collum, double price, Int64 type, string isAvailable)
    {
        Id = id;
        AuditoriumId = Auditorium_ID;
        Row = (int)row;
        Collum = (int)collum;
        Price = price;
        Class = (int)type;
        IsAvailable = isAvailable == "";
    }
}



