public class SnacksModel
{
    public string Name { get; set; }
    public float Price { get; set; }
    
    public SnacksModel(string name, float price)
    {
        Name = name;
        Price = price; 
    }


}