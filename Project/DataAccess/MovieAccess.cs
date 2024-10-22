using Microsoft.Data.Sqlite;

using Dapper;


public static class MovieAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Movies";

    public static void Write(MovieModel movie)
    {
        string sql = $"INSERT INTO {Table} (name, author, description, length, genre, age_rating, movie_ratings) VALUES (@Name, @Author, @Description, @Length ,@Genre, @AgeRating, @MovieRating)";
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = sql;

            // Add parameters for the SQL query
            command.Parameters.AddWithValue("@Name", movie.Name);
            command.Parameters.AddWithValue("@Author", movie.Author);
            command.Parameters.AddWithValue("@Description", movie.Description);
            command.Parameters.AddWithValue("@Length", movie.Length);
            command.Parameters.AddWithValue("@Genre", movie.Genre);
            command.Parameters.AddWithValue("@AgeRating", movie.AgeRating);
            command.Parameters.AddWithValue("@MovieRating", movie.MovieRating);

            // Open connection and execute the query
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        //_connection.Execute(sql, movie);
    }


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
                        reader.GetString(7)    // MovieRating
                    );
                }
            }
            _connection.Close();
        }

        return null;  // Return null if no movie found

    }

    public static void Update(MovieModel movie, int id)
    {
        string sql = $"UPDATE {Table} SET name = @Name, author = @Author, description = @Description, length = @Length, genre = @Genre, age_rating = @AgeRating, movie_ratings = @MovieRating WHERE id = @Id";
        _connection.Execute(sql, new
        {
            movie.Name,
            movie.Author,
            movie.Description,
            movie.Length,
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