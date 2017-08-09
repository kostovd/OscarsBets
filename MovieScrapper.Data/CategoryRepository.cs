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
                    .Include("UsersWatchedThisMovie")
                    //.Where(x => x.UsersWatchedThisMovie.Any())
                    .ToList();
                return movies;
            }
        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)
        {

            using (var ctx = new MovieContext())
            {
                var watched = ctx.Watched.Include("Movies").Where(x => x.UserId == userId).ToList();

                return watched;
            }
        }

        

        public Category GetCategory(int id)
        {

            using (var ctx = new MovieContext())
            {
                var foundedCategory = ctx.MovieCaterogries.Include("Movies").Where(x=>x.Id==id).SingleOrDefault();
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

        public IEnumerable<Movie> GetAllMoviesInCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedCategoty = ctx.MovieCaterogries.Include("Movies").Where(x => x.Id==categoryId).SingleOrDefault();
                return foundedCategoty.Movies;
            }
        }

        public void AddCategory(Category category)
        {
            using (var ctx = new MovieContext())
            {                
                ctx.MovieCaterogries.Add(category);
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
        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            using (var ctx = new MovieContext())
            {
                watchedEntity = ctx.Watched.Add(watchedEntity);
                ctx.SaveChanges();
            }

            return watchedEntity;
        }

        

        //public void AddWatchedMovie(Watched watchedEntity, int movieId)
        //{

        //    using (var ctx = new MovieContext())
        //    {
        //        var movie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);

        //        watchedEntity = ctx.Watched.Attach(watchedEntity);
        //        watchedEntity.Movies.Add(movie);
        //        ctx.SaveChanges();

        //    }
        //} 

        public void AddWatchedMovie(string userId, int movieId)
        {

            using (var ctx = new MovieContext())
            {
                var movie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var watchedEntity = ctx.Watched.Include("Movies").SingleOrDefault(x => x.UserId == userId);               
                watchedEntity.Movies.Add(movie);
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
                var databaseMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").SingleOrDefault(x => x.Id == categoryId);
                databaseCategory.Movies.Add(databaseMovie);
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

        public void RemoveMovieFromCategory(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var databaseMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").SingleOrDefault(x => x.Id == categoryId);
                var foundedMovie = databaseCategory.Movies.FirstOrDefault(x => x.Id == movieId);
                databaseCategory.Movies.Remove(foundedMovie);
                ctx.SaveChanges();
            }
        }

        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var databaseMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").SingleOrDefault(x => x.Id == categoryId);
                var foundedMovie = databaseCategory.Movies.FirstOrDefault(x => x.Id == movieId);
                return foundedMovie;
            }
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedEntity = ctx.Watched.Where(x=> x.UserId==userId).SingleOrDefault();
                return foundedEntity;
                
            }
        }
        
    }
}
