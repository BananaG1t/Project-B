public class ScheduleModel
{

    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public MovieModel Movie { get; set; }

    public int MovieId { get; set; }

    public AuditoriumModel Auditorium { get; set; }

    public int AuditoriumId { get; set; }

    public int LocationId { get; set; }
    public LocationModel Location { get; set; }

    public ScheduleModel(Int64 id, string startTime, string endTime, Int64 Movie_ID, Int64 Auditorium_ID, Int64 Location_ID)
    {
        string format = "yyyy-MM-dd HH:mm:ss";
        Id = (int)id;
        DateTime.TryParseExact(startTime, format, null, System.Globalization.DateTimeStyles.None, out DateTime output);
        StartTime = output;
        DateTime.TryParseExact(endTime, format, null, System.Globalization.DateTimeStyles.None, out output);
        EndTime = output;
        MovieId = (int)Movie_ID;
        Movie = MovieAccess.GetById(MovieId) ?? throw new Exception("Movie not found"); // can't be null because of foreign key
        AuditoriumId = (int)Auditorium_ID;
        Auditorium = AuditoriumAcces.GetById(AuditoriumId) ?? throw new Exception("Auditorium not found"); // can't be null because of foreign key
        LocationId = (int)Location_ID;
        Location = LocationAccess.GetById(LocationId) ?? throw new Exception("Location not found"); // can't be null because of foreign key
    }

    public ScheduleModel(DateTime startTime, MovieModel movie, AuditoriumModel auditorium, LocationModel location)
    {
        StartTime = startTime;
        Movie = movie;
        MovieId = Movie.Id;
        Auditorium = auditorium;
        AuditoriumId = Auditorium.Id;
        EndTime = StartTime + Movie.Length;
        LocationId = location.Id;
        Location = location;
        Id = ScheduleAccess.Write(this);
    }


}



