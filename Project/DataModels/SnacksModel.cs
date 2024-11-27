using System.Data.Common;

public class SnacksModel
{
    public string Name { get; set; }
    public double Price { get; set; }
    public Int64 Id { get; set; }
    public SnacksModel(Int64 id ,string name, double price)
    {
        Id = id;
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