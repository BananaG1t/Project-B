static class SnackReservation
{
    public static void ManageSnacks(string name, float price)
    {
        float price;
        do while(price < 0)
        {
            Console.WriteLine("What snack would you like to add")
            string name = Console.ReadLine()
            Console.WriteLine("What is the price of the snack")
            price = float.Parse(Console.Readline())
            
            if (price < 0) {Console.WriteLine("The price is incorrent")}
            else {Console.WriteLine("snack added")}
        }
    }
}