public class MovieModel
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Length { get; set; }
    public string Genre { get; set; }
    public string MovieRating { get; set; }

    public MovieModel(string name, string author, string description, TimeSpan length, string genre, string movierating)
    {
        Name = name;
        Author = author;
        Description = description;
        Length = length.ToString();
        Genre = genre;
        MovieRating = movierating;
    }
}



