using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class MovieLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static MovieModel? CurrentMovie { get; private set; }

    public MovieLogic()
    {
        // Could do something here

    }

    public MovieModel GetById(int id)
    {
        return MovieAccess.GetById(id);
    }

    public static List<MovieModel> GetAll()
    {
        return MovieAccess.GetAll();
    }

    public void AddMovie(string name, string author, string description, TimeSpan length, string genre, int agerating, double movierating)
    {
        MovieAccess.Write(new MovieModel(name, author, description, length, genre, agerating, movierating));
    }

    public void UpdateMovie(string name, string author, string description, TimeSpan length, string genre, int agerating, double movierating, int id)
    {
        MovieAccess.Update(new MovieModel(name, author, description, length, genre, agerating, movierating), id);
    }

    public void DeleteMovie(int id) { MovieAccess.Delete(id); }
}


