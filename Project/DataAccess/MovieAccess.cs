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

        using (var command = _connection.CreateCommand())
        {
            command.CommandText = sql;
            command.Parameters.AddWithValue("@Id", id);

            _connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Retrieve the TIME value from the database as a TimeSpan
                    TimeSpan lengthAsTimeSpan = reader.GetTimeSpan(4);  // Get the TIME value as TimeSpan

                    // Create and return a new MovieModel instance
                    return new MovieModel(
                        reader.GetString(1),   // Name
                        reader.GetString(2),   // Author
                        reader.GetString(3),   // Description
                        lengthAsTimeSpan,      // Length as TimeSpan
                        reader.GetString(5),   // Genre
                        reader.GetInt32(6),    // AgeRating
                        reader.GetDouble(7)    // MovieRating
                    );
                }
            }
            _connection.Close();
        }

        return null;  // Return null if no movie found
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