class ReservationLogic
{
    public ReservationLogic()
    {

    }
    public static void Start()
    {
        List<string> AvailibleDays = DisplayDays();

        string text = string.Join("\n", AvailibleDays);
        List<int> ValidAnswerList = General.ListMaker(1, 8);

        int input = General.ValidAnswer(text, ValidAnswerList);

        string ChosenDay = AvailibleDays[input - 1];
        ChosenDay = ChosenDay.Remove(0, 4);

        Reservation.PickTime(ChosenDay);
    }

    public static List<string> DisplayDays()
    {
        List<string> DateInfo = [];

        for (int i = 0; i < 8; i++)
        {
            string DateInfoText = "";

            DateInfoText += $"[{i + 1}] ";
            DateInfoText += DateTime.Today.AddDays(i).ToString("d/M/yyyy");

            DateInfo.Add(DateInfoText);
        }
        return DateInfo;
    }

}