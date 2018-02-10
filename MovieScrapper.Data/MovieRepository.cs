using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MovieScrapper.Entities;
using MovieScrapper.Data.Interfaces;

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

        public void OverrideMovie(Movie movie)
        {
            using (var ctx = new MovieContext())
            {
                ctx.Entry(movie).State = EntityState.Modified;

                foreach (MovieCredit credit in movie.Credits)
                {
                    if (ctx.Credits.Any(x => x.Id == credit.Id))
                    {
                        ctx.Entry(credit).State = EntityState.Modified;
                    }
                    else
                    {
                        ctx.Entry(credit).State = EntityState.Added;
                    }
                }

                ctx.SaveChanges();
            }
        }

        public void ChangeMovieStatus(string userId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var movie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var watchedEntity = ctx.Watched.Include(w => w.Movies).SingleOrDefault(x => x.UserId == userId);
                if (watchedEntity.Movies.Any(m => m.Id == movieId))
                {
                    watchedEntity.Movies.Remove(movie);
                }
                else
                {
                    watchedEntity.Movies.Add(movie);
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
                    .Where(x => x.Nominations.Any())
                    .OrderBy(m=>m.Title)
                    .ToList();

                return movies;
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
