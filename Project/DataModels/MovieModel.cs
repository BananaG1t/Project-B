public class MovieModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public TimeSpan Length { get; set; }
    public string Genre { get; set; }
    public decimal MovieRating { get; set; }
    public int AgeRating { get; set; }

    public MovieModel(Int64 id, string name, string author, string description, TimeSpan length, string genre, int agerating, string movierating)
    {
        Id = id;
        Name = name;
        Author = author;
        Description = description;
        Length = length;
        Genre = genre;
        AgeRating = agerating;
        MovieRating = movierating;
    }

    public MovieModel(string name, string author, string description, TimeSpan length, string genre, int agerating, decimal movierating)
    {
        Name = name;
        Author = author;
        Description = description;
        Length = length;
        Genre = genre;
        AgeRating = agerating;
        MovieRating = movierating;
        Id = MovieAccess.Write(this);
    }
}



