using System.Data.Common;

public class SnacksModel
{
    public string Name { get; set; }
    public float Price { get; set; }
    public Int64 Id { get; set; }
    public SnacksModel(Int64 id ,string name, float price)
    {
        Id = id;
        Name = name;
        Price = price; 
    }

    public SnacksModel(string name, float price)
    {
        Name = name;
        Price = price;
        Id = SnacksAccess.Write(this);
    }


}