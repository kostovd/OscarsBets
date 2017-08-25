using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MovieScrapper.Data
{
    public class CategoryRepository
    {
# region ADD
        public void AddCategory(Category category)
        {
            using (var ctx = new MovieContext())
            {
                ctx.MovieCaterogries.Add(category);
                ctx.SaveChanges();
            }
        }

        public void AddMovie(Movie movie)
        {
            using (var ctx = new MovieContext())
            {
                ctx.Movies.Add(movie);
                ctx.SaveChanges();
            }
        }

        public void AddMovieInCategory(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var foundedCategory = ctx.MovieCaterogries.Include(cat => cat.Movies).SingleOrDefault(cat => cat.Id == categoryId);
                foundedCategory.Movies.Add(foundedMovie);
                ctx.SaveChanges();
            }
        }

        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            using (var ctx = new MovieContext())
            {
                watchedEntity = ctx.Watched.Add(watchedEntity);
                ctx.SaveChanges();
            }

            return watchedEntity;
        }

        public bool AreWinnersSet()
        {
            using (var ctx = new MovieContext())
            {
                var winners = ctx.MovieCaterogries.Select(x => x.Winner);
                if (winners != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
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

        public void ChangeGameStopDate(DateTime stopDate)
        {
            using (var ctx = new MovieContext())
            {
                var foundedDate = ctx.StopDate.FirstOrDefault();
                if (foundedDate == null)
                {
                    var stopDateEntity = new StopDate {StopGameDate=stopDate };
                    ctx.StopDate.Add(stopDateEntity);
                }
                else
                {
                    foundedDate.StopGameDate = stopDate;
                }

                ctx.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries.Where(x => x.Id == id).SingleOrDefault();
                ctx.Entry(databaseCategory).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public void EditCategory(Category category)
        {
            using (var ctx = new MovieContext())
            {
                ctx.Entry(category).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        #region GET


        public IEnumerable<Category> GetAll()
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries
                    .Include(cat => cat.Movies)
                    .Include(cat => cat.Bets)
                    .ToList();

                return databaseCategory;

            }
        }
        public IEnumerable<Movie> GetAllMovies()
        {

            using (var ctx = new MovieContext())
            {
                var movies = ctx.Movies
                    .Include(u=>u.UsersWatchedThisMovie)
                    //.Where(x => x.UsersWatchedThisMovie.Any())
                    .ToList();
                return movies;
            }
        }

        public IEnumerable<Movie> GetAllMoviesInCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedCategoty = ctx.MovieCaterogries.Include(cat => cat.Movies).Where(cat => cat.Id == categoryId).SingleOrDefault();
                return foundedCategoty.Movies;
            }
        }

        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var bets = ctx.Bets.Where(bet => bet.UserId == userId).ToList();
                return bets;
            }
        }

        //public IEnumerable<string> GetAllUsersWatchedThisMovie (int movieId)
        //{
        //    using (var ctx = new MovieContext())
        //    {
        //        var users = ctx.Watched.Include(m=>m.Movies).Where(w=>w.Movies.)
        //        return users;
        //    }
        //}
        public IEnumerable<Watched> GetAllUsersWatchedAMovie()
        {

            using (var ctx = new MovieContext())
            {
                var watched = ctx.Watched.Include(w => w.Movies);

                return watched;
            }
        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)
        {

            using (var ctx = new MovieContext())
            {
                var watched = ctx.Watched.Include(w => w.Movies).Where(x => x.UserId == userId).ToList();

                return watched;
            }
        }

        public Category GetCategory(int id)
        {

            using (var ctx = new MovieContext())
            {
                var foundedCategory = ctx.MovieCaterogries.Include(cat => cat.Movies).Where(cat => cat.Id == id).SingleOrDefault();
                return foundedCategory;
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

        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var databaseMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var databaseCategory = ctx.MovieCaterogries.Include(cat => cat.Movies).SingleOrDefault(x => x.Id == categoryId);
                var foundedMovie = databaseCategory.Movies.FirstOrDefault(x => x.Id == movieId);
                return foundedMovie;
            }
        }
        
        public StopDate GetStopDate()
        {
            using (var ctx = new MovieContext())
            {
                var foundedDate = ctx.StopDate.First();
                return foundedDate;
            }
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedEntity = ctx.Watched.Where(x => x.UserId == userId).SingleOrDefault();
                return foundedEntity;

            }
        }
        public Bet GetUserBetEntity(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedEntity = ctx.Bets.Where(x => x.UserId == userId).SingleOrDefault();
                return foundedEntity;

            }
        }

#endregion

       

        public Bet MakeBetEntity(string userId, int movieId, int categoryId)
        {
            using (var ctx = new MovieContext())
            {                
                var foundedMovie = ctx.Movies.Where(m => m.Id == movieId).SingleOrDefault();
                var foundedCategory = ctx.MovieCaterogries.Where(x => x.Id == categoryId).SingleOrDefault();
                var foundedUserBets = ctx.Bets.Where(x => x.UserId == userId);
                Bet foundedUserBetInThisCategory = ctx.Bets.Where(x => x.UserId == userId).Where(y => y.Category.Id == categoryId).FirstOrDefault();
                if (foundedUserBetInThisCategory == null)
                {
                    var betEntity = new Bet() { UserId = userId, Movie = foundedMovie, Category = foundedCategory };
                    betEntity = ctx.Bets.Add(betEntity);
                    ctx.SaveChanges();
                    return betEntity;
                }
                else
                {
                    foundedUserBetInThisCategory.Movie = foundedMovie;
                    ctx.SaveChanges();
                    return foundedUserBetInThisCategory;
                }
            }         
        }

        public void MarkAsWinner(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var foundedCategory = ctx.MovieCaterogries.Include(cat => cat.Winner).SingleOrDefault(x => x.Id == categoryId);
                foundedCategory.Winner = foundedMovie;
                ctx.SaveChanges();
            }
        }

        public void RemoveMovieFromCategory(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var databaseMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var databaseCategory = ctx.MovieCaterogries.Include(cat => cat.Movies).SingleOrDefault(x => x.Id == categoryId);
                var foundedMovie = databaseCategory.Movies.FirstOrDefault(x => x.Id == movieId);
                databaseCategory.Movies.Remove(foundedMovie);
                ctx.SaveChanges();
            }
        }                               

    }
}
