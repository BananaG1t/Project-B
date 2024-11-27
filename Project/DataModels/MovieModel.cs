public class MovieModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public string Description { get; set; }
    public string LengthAsString { get; set; }
    public TimeSpan Length { get; set; }
    public string Genre { get; set; }
    public double MovieRating { get; set; }
    public int AgeRating { get; set; }
    public Int64 Day { get; set; }
    public object Day1 { get; }

    public MovieModel(Int64 id, string name, string director, string description, string length, string genre, Int64 age_rating, double movie_ratings, Int64 day)
    {
        Id = id;
        Name = name;
        Director = director;
        Description = description;
        Length = TimeSpan.Parse(length);
        Genre = genre;
        AgeRating = (int)age_rating;
        MovieRating = movie_ratings;
        Day = day;
    }

    public MovieModel(string name, string director, string description, TimeSpan length, string genre, int age_rating, double movie_ratings)
    {
        Name = name;
        Director = director;
        Description = description;
        Length = length;
        Genre = genre;
        AgeRating = age_rating;
        MovieRating = movie_ratings;
        Id = MovieAccess.Write(this);
    }

    public MovieModel(object day, string name, string director, string description, TimeSpan length, string genre, int agerating, double movierating)
    {
        Day1 = day;
        Name = name;
        Director = director;
        Description = description;
        Length = length;
        Genre = genre;
        AgeRating = agerating;
        MovieRating = movierating;
        Id = MovieAccess.Write(this);
    }

    // public MovieModel()
    // {

    // }
}



