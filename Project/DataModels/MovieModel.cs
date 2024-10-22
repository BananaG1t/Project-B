public class MovieModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public TimeSpan Length { get; set; }
    public string Genre { get; set; }
    public double MovieRating { get; set; }
    public int AgeRating { get; set; }

    public MovieModel(Int64 id, string name, string author, string description, string length, string genre, Int64 age_rating, double movie_ratings)
    {
        Id = id;
        Name = name;
        Author = author;
        Description = description;
        Length = TimeSpan.Parse(length);
        Genre = genre;
        AgeRating = (int)age_rating;
        MovieRating = movie_ratings;
    }

    public MovieModel(string name, string author, string description, TimeSpan length, string genre, int age_rating, double movie_ratings)
    {
        Name = name;
        Author = author;
        Description = description;
        Length = length;
        Genre = genre;
        AgeRating = age_rating;
        MovieRating = movie_ratings;
        Id = MovieAccess.Write(this);
    }
}



