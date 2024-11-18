Public class SnackModel
{
    public string Name { get; set; }
    public float Price { get; set; }
    
    public SnackModel(string name, float price)
    {
        Name = name;
        Price = price; 
    }


}