public class MovieModel
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public DateTime Length { get; set; }
    public string Genre { get; set; }
    public string MovieRating { get; set; }

    public MovieModel(Int64 id, string name, string author, string description, DateTime length, string genre, string movierating)
    {
        Name = name;
        Author = author;
        Description = description;
        Length = length;
        Genre = genre;
        MovieRating = movierating;
    }


}



