using Microsoft.Data.Sqlite;

using Dapper;


public static class MovieAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Movies";

    public static Int64 Write(MovieModel movie)
    {
        movie.Length.ToString(@"hh\:mm\:ss");
        string sql = $"INSERT INTO {Table} (name, author, description, length, genre, age_rating, movie_ratings) VALUES (@Name, @Author, @Description, @Length ,@Genre, @AgeRating, @MovieRating)";
        _connection.Execute(sql, movie);

        string idSql = "SELECT last_insert_rowid();";
        Int64 lastId = _connection.ExecuteScalar<Int64>(idSql);

        return lastId;
    }

    public static MovieModel GetById(int id)
    {
        string sql = $"SELECT id, name, author, description, length, genre, age_rating, CAST(movie_ratings AS REAL) AS movie_ratings FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<MovieModel>(sql, new { Id = id });
    }

    public static List<MovieModel> GetAll()
    {
        string sql = $"SELECT id, name, author, description, length, genre, age_rating, CAST(movie_ratings AS REAL) AS movie_ratings FROM {Table}";
        List<MovieModel> Movies = (List<MovieModel>)_connection.Query<MovieModel>(sql);

        return Movies;
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