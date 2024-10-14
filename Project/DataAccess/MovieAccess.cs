using Microsoft.Data.Sqlite;

using Dapper;


public static class MovieAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private static string Table = "Movies";

    public static void Write(MovieModel movie)
    {
        string sql = $"INSERT INTO {Table} (name, author, description, length, genre, movierating) VALUES (@Name, @Author, @Description, @Length ,@Genre ,@Movie_ratings)";
        _connection.Execute(sql, movie);
    }


    public static MovieModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<MovieModel>(sql, new { Id = id });
    }

    public static MovieModel GetByName(string name)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<MovieModel>(sql, new { Name = name });
    }

    public static void Update(MovieModel movie)
    {
        string sql = $"UPDATE {Table} SET name = @Name, author = @Author, description = @Description, length = @Length, genre = @Gnere, movierating = @Movie_ratings WHERE id = @Id";
        _connection.Execute(sql, movie);
    }

    public static void Delete(int id)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = id });
    }



}