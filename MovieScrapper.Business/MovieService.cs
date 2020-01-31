using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Business.Enums;

namespace MovieScrapper.Business
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {
            _movieRepository.ChangeMovieStatus(userId, movieId);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        public IEnumerable<Movie> GetAllMoviesByCriteria(OrderType orderType)
        {
            IEnumerable<Movie> allMovies = new List<Movie>();

            switch (orderType)
            {
                case OrderType.ByName:
                    allMovies = GetAllMovies();
                    break;

                case OrderType.ByNominations:
                    allMovies = MoviesByNominations();
                    break;

                case OrderType.ByProxiadPopularity:
                    allMovies = MoviesByProxiadPopularity();
                    break;
            }

            return allMovies;
        }

        private IEnumerable<Movie> MoviesByNominations()
        {
            return GetAllMovies()
                .OrderByDescending(x => x.Nominations.Count)
                .ThenByDescending(x => x.UsersWatchedThisMovie.Count)
                .ThenByDescending(x => x.Title)
                .ToList();
        }

        private IEnumerable<Movie> MoviesByProxiadPopularity()
        {
            return GetAllMovies()
                .OrderByDescending(x => x.UsersWatchedThisMovie.Count)
                .ThenByDescending(x => x.Nominations.Count)
                .ThenByDescending(x => x.Title)
                .ToList();
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
