using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MovieScrapper.Entities;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities.Interfaces;

namespace MovieScrapper.Data
{
    public class MovieRepository: IMovieRepository
    {

        public void AddMovie(Movie movie)
        {
            using (var ctx = new MovieContext())
            {
                ctx.Movies.Add(movie);
                ctx.SaveChanges();
            }
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {

            using (var ctx = new MovieContext())
            {
                var movie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var watchedEntity = ctx.Watched.Include(w => w.Movies).SingleOrDefault(x => x.UserId == userId);
                if (watchedEntity.Movies.Where(m => m.Id == movieId).SingleOrDefault() == null)
                {
                    watchedEntity.Movies.Add(movie);
                }
                else
                {
                    watchedEntity.Movies.Remove(movie);
                }

                ctx.SaveChanges();
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {

            using (var ctx = new MovieContext())
            {
                var movies = ctx.Movies
                    .Include(u => u.UsersWatchedThisMovie)
                    //.Where(x => x.UsersWatchedThisMovie.Any())
                    .ToList();
                return movies;
            }
        }

        public IEnumerable<Movie> GetAllMoviesInCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedCategoty = ctx.MovieCaterogries
                    .Include(cat => cat.Winner)
                    .Include(cat => cat.Movies)
                    .Where(cat => cat.Id == categoryId).SingleOrDefault();
                return foundedCategoty.Movies;
            }
        }

        public Movie GetMovie(int id)
        {

            using (var ctx = new MovieContext())
            {
                var foundedMovie = ctx.Movies.Where(m => m.Id == id).SingleOrDefault();
                return foundedMovie;
            }
        }

        public bool HasMovie(int id)
        {
            using (var ctx = new MovieContext())
            {
                var isMovieFound = ctx.Movies.Any(m => m.Id == id);
                return isMovieFound;
            }
        }

    }
}
