using MovieScrapper.Entities;
using System.Collections.Generic;

namespace MovieScrapper.Data.Interfaces
{
    public interface IMovieRepository
    {

        void AddMovie(Movie movie);
        
        void ChangeMovieStatus(string userId, int movieId);

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<Movie> GetAllMoviesInCategory(int categoryId);

        Movie GetMovie(int id);

        bool HasMovie(int id);

    }
}
