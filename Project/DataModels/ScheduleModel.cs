public class ScheduleModel
{

    public Int64 Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public MovieModel Movie { get; set; }

    public int MovieId { get; set; }

    public AuditoriumModel Auditorium { get; set; }

    public int AuditoriumId { get; set; }

    public ScheduleModel(Int64 id, string startTime, string endTime, Int64 Movie_ID, Int64 Auditorium_ID)
    {
        string format = "yyyy-MM-dd HH:mm:ss";
        Id = id;
        DateTime.TryParseExact(startTime, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        StartTime = output;
        DateTime.TryParseExact(endTime, format, null, System.Globalization.DateTimeStyles.None, out output);
        EndTime = output;
        MovieId = (int)Movie_ID;
        Movie = MovieAccess.GetById(MovieId);
        AuditoriumId = (int)Auditorium_ID;
        Auditorium = AuditoriumAcces.GetById(AuditoriumId);
    }

    public ScheduleModel(DateTime startTime, MovieModel movie, AuditoriumModel auditorium)
    {
        StartTime = startTime;
        Movie = movie;
        MovieId = (int)Movie.Id;
        Auditorium = auditorium;
        AuditoriumId = (int)Auditorium.Id;
        EndTime = StartTime + Movie.Length;
        Id = ScheduleAccess.Write(this);
    }


}



