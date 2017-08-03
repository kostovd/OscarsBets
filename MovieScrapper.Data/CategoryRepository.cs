using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Data
{
    public class CategoryRepository
    {
        public IEnumerable<MovieCategory> GetAll()
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").ToList();
                return databaseCategory;
            }
        }

        public MovieCategory GetCategory(int id)
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").Where(x=>x.Id==id).SingleOrDefault();
                return databaseCategory;
            }
        }

        public Movie GetMovie(string id)
        {

            using (var ctx = new MovieContext())
            {
                var databaseMovie = ctx.Movies.Where(m => m.Id == id).SingleOrDefault();
                return databaseMovie;
            }
        }

        public IEnumerable<Movie> GetAllMoviesInCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                var databaseCategoty = ctx.MovieCaterogries.Include("Movies").Where(x => x.Id==categoryId).SingleOrDefault();
                return databaseCategoty.Movies;
            }
        }

        public void AddCategory(MovieCategory category)
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

        public void AddMovieInCategory(int categoryId, string movieId)
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

        public void RemoveMovieFromCategory(int categoryId, string movieId)
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

        public Movie GetMovieInCategory(int categoryId, string movieId)
        {
            using (var ctx = new MovieContext())
            {
                var databaseMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").SingleOrDefault(x => x.Id == categoryId);
                var foundedMovie = databaseCategory.Movies.FirstOrDefault(x => x.Id == movieId);
                return foundedMovie;
            }
        }
    }
}
