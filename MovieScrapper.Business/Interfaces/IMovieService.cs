using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business.Interfaces
{
    public interface IMovieService
    {
        void AddMovie(Movie movie);

        void ChangeMovieStatus(string userId, int movieId);

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<Movie> GetAllMoviesInCategory(int categoryId);

        Movie GetMovie(int id);

        bool HasMovie(int id);
        
    }
}
