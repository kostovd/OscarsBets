using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class MovieService: IMovieService
    {
        public void AddMovie(Movie movie)
        {
            var repo = new MovieRepository();
            repo.AddMovie(movie);
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {
            var repo = new MovieRepository();
            repo.ChangeMovieStatus(userId, movieId);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var repo = new MovieRepository();
            return repo.GetAllMovies();
        }

        public IEnumerable<Movie> GetAllMoviesInCategory(int categoryId)
        {
            var repo = new MovieRepository();
            return repo.GetAllMoviesInCategory(categoryId);
        }

        public Movie GetMovie(int id)
        {
            var repo = new MovieRepository();
            return repo.GetMovie(id);
        }
    }
}
