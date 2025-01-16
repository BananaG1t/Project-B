using Microsoft.Data.Sqlite;

using Dapper;


public static class MovieAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static readonly string Table = "Movies";

    public static int Write(MovieModel movie)
    {
        movie.Length.ToString(@"hh\:mm\:ss");
        string sql = $"INSERT INTO {Table} (name, director, description, length, genre, age_rating, movie_ratings) VALUES (@Name, @Director, @Description, @Length ,@Genre, @AgeRating, @MovieRating)";
        _connection.Execute(sql, movie);

        string idSql = "SELECT last_insert_rowid();";
        int lastId = _connection.ExecuteScalar<int>(idSql);

        return lastId;
    }

    public static MovieModel? GetById(int id)
    {
        string sql = $"SELECT id, name, director, description, length, genre, age_rating, CAST(movie_ratings AS REAL) AS movie_ratings FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<MovieModel>(sql, new { Id = id });
    }

    public static List<MovieModel> GetAll()
    {
        string sql = $"SELECT id, name, director, description, length, genre, age_rating, CAST(COALESCE(movie_ratings, 0.0) AS REAL) AS movie_ratings FROM {Table}";
        List<MovieModel> Movies = (List<MovieModel>)_connection.Query<MovieModel>(sql);

        return Movies;
    }

    public static void Update(MovieModel movie, int id)
    {
        string sql = $"UPDATE {Table} SET name = @Name, director = @Director, description = @Description, length = @Length, genre = @Genre, age_rating = @AgeRating, movie_ratings = @MovieRating WHERE id = @Id";
        _connection.Execute(sql, new
        {
            movie.Name,
            movie.Director,
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

    public static int GetAuditoriumById(int movie_id) // added method to get auditorium id
    {
        string sql = $"SELECT Auditorium_ID FROM {Table} WHERE Movie_ID = @Movie_ID";
        return _connection.QueryFirstOrDefault<int>(sql, new { Movie_ID = movie_id });
    }

    public static int GetTotalSeats(int auditorium_id)
    {
        string sql = $"SELECT * FROM {Table} WHERE Movie_ID = @Movie_ID";
        return _connection.QueryFirstOrDefault<int>(sql, new { Auditorium_ID = auditorium_id });
    }
}