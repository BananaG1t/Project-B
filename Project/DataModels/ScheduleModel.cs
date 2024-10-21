public class ScheduleModel
{

    public Int64 Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public MovieModel Movie { get; set; }

    public AuditoriumModel Auditorium { get; set; }

    public ScheduleModel(Int64 id, DateTime startTime, DateTime endTime, MovieModel movie, AuditoriumModel auditorium)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        Movie = movie;
        Auditorium = auditorium;
    }

    public ScheduleModel(DateTime startTime, MovieModel movie, AuditoriumModel auditorium)
    {
        StartTime = startTime;
        Movie = movie;
        Auditorium = auditorium;
    }


}



