using System.Data.Common;

public class SnacksModel
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int MovieId { get; set; }
    public int ReservationId { get; set; }
    public double Amount { get; set; }

    public SnacksModel(Int64 id ,string name, double price)
    {
        Id = (int)id;
        Name = name;
        Price = price; 
    }

    public SnacksModel(string name, double price)
    {
        Name = name;
        Price = price;
        Id = SnacksAccess.Write(this);
    }
}