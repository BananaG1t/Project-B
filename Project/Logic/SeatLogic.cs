//This class is not static so later on we can use inheritance and interfaces
public class SeatLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static SeatModel? CurrentSeat { get; private set; }

    public SeatLogic()
    {
        // Could do something here
    }

    public static SeatModel? GetByReservationInfo(int ColNum, int RowNum, int Auditorium_ID)
    {
        return SeatsAccess.GetByReservationInfo(ColNum, RowNum, Auditorium_ID);
    }

    public static void Update(SeatModel seat)
    {
        SeatsAccess.Update(seat);
    }

    public static (int newRow, int newRol) FindNextSeat(Dictionary<(int Row, int Collum), SeatModel> seats, int row, int col, int amount, string direction)
    {
        int maxRow = seats.Keys.Max(k => k.Row);
        int maxCol = seats.Keys.Max(k => k.Collum);
        int newRow = row;
        int newCol = col;

        bool isValid(int row, int col)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!seats.ContainsKey((row, col + i)) || !seats[(row, col + i)].IsAvailable)
                    return false;
            }
            return true;
        }

        while (true)
        {
            if (direction == "down")
                newRow += 1;
            else if (direction == "up")
                newRow -= 1;

            if (newRow > maxRow || newRow < 1)
                return (row, col);

            bool left = true;
            bool right = true;
            int cnt = 0;

            if (direction == "left")
            {
                right = false;
                cnt++;
            }
            else if (direction == "right")
            {
                left = false;
                cnt++;
            }

            while (true)
            {
                if (newCol + (amount - 1) + cnt > maxCol)
                {
                    right = false;
                }
                if (newCol - cnt < 1)
                {
                    left = false;
                }

                if (!right && !left)
                    break;

                if (right)
                    if (isValid(newRow, newCol + cnt))
                    {
                        return (newRow, newCol + cnt);
                    }
                if (left)
                    if (isValid(newRow, newCol - cnt))
                    {
                        return (newRow, newCol - cnt);
                    }
                cnt++;
            }
            if (direction == "left" || direction == "right")
                return (row, col);
        }
    } 
}


