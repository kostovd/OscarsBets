using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieScrapper.Data.Interfaces;

namespace MovieScrapper.Business
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;       

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void AddMovie(Movie movie)
        {
            _movieRepository.AddMovie(movie);
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {
            _movieRepository.ChangeMovieStatus(userId, movieId);
        }

        public IEnumerable<Movie> GetAllMovies()
        {

            return _movieRepository.GetAllMovies();
        }

        public Movie GetMovie(int id)
        {
            return _movieRepository.GetMovie(id);
        }

        public bool HasMovie(int id)
        {
            return _movieRepository.HasMovie(id);
           
        }
    }
}
