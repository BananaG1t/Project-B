using Microsoft.Data.Sqlite;

using Dapper;


public static class MovieAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Movies";

    public static int Write(MovieModel movie)
    {
        movie.Length.ToString(@"hh\:mm\:ss");
        string sql = $"INSERT INTO {Table} (name, author, description, length, genre, age_rating, movie_ratings) VALUES (@Name, @Author, @Description, @Length ,@Genre, @AgeRating, @MovieRating)";
        _connection.Execute(sql, movie);

        string selectSql = "SELECT last_insert_rowid();";
    
        int newId = _connection.ExecuteScalar<int>(selectSql);

        return newId; 

    }


    // public static MovieModel GetById(int id)
    // {
    //     string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        
    //     var movie = _connection.QueryFirstOrDefault(sql, new { Id = id });

    //     if (movie != null)
    //     {
    //         // Map the movie object to MovieModel and handle TimeSpan conversion
    //         return new MovieModel(
    //             movie.Id,
    //             movie.Name,
    //             movie.Author,
    //             movie.Description,
    //             TimeSpan.Parse(movie.Length),  // Convert length from string to TimeSpan
    //             movie.Genre,
    //             movie.AgeRating,
    //             movie.MovieRating
    //         );
    //     }
        
    //     return null;  // Return null if no movie found

    // }


    public static MovieModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        
        return _connection.QueryFirstOrDefault<MovieModel>(sql, new { Id = id });

    }

    public static void Update(MovieModel movie, int id)
    {
        string sql = $"UPDATE {Table} SET name = @Name, author = @Author, description = @Description, length = @Length, genre = @Genre, age_rating = @AgeRating, movie_ratings = @MovieRating WHERE id = @Id";
        _connection.Execute(sql, new
        {
            movie.Name,
            movie.Author,
            movie.Description,
            Length = movie.Length.ToString(@"hh\:mm\:ss"),  
            movie.Genre,
            movie.AgeRating,
            movie.MovieRating,
            Id = id
        });
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }

}