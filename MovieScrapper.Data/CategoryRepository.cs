using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MovieScrapper.Data.Interfaces;

namespace MovieScrapper.Data
{
    public class CategoryRepository: ICategoryRepository
    {
        public void AddCategory(Category category)
        {
            using (var ctx = new MovieContext())
            {
                ctx.MovieCaterogries.Add(category);
                ctx.SaveChanges();
            }
        }
        

        public void AddMovie(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {
                var foundedMovie = ctx.Movies.SingleOrDefault(x => x.Id == movieId);
                var foundedCategory = ctx.MovieCaterogries.Include(cat => cat.Movies).SingleOrDefault(cat => cat.Id == categoryId);
                foundedCategory.Movies.Add(foundedMovie);
                ctx.SaveChanges();
            }
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
       
        public IEnumerable<Category> GetAll()
        {
            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries
                    .Include(cat => cat.Movies)
                    .Include(cat => cat.Bets.Select(bet=>bet.Movie))
                    .Include(cat => cat.Winner)
                    .OrderBy(cat=>cat.Id)
                    .ToList();

                return databaseCategory;
            }
        }                                          

        public Category GetCategory(int id)
        {
            using (var ctx = new MovieContext())
            {
                var foundedCategory = ctx.MovieCaterogries
                    .Include(cat =>cat.Winner)
                    .Include(cat => cat.Movies).Where(cat => cat.Id == id).SingleOrDefault();
                return foundedCategory;
            }
        }
               
        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {              
                var databaseCategory = ctx.MovieCaterogries.Include(cat => cat.Movies).SingleOrDefault(x => x.Id == categoryId);
                var foundedMovie = databaseCategory.Movies.FirstOrDefault(x => x.Id == movieId);
                return foundedMovie;
            }
        }

        public bool HasMovieInCategory(int categoryId, int movieId)
        {
            using (var ctx = new MovieContext())
            {           
                return ctx.MovieCaterogries.Any(x => x.Id == categoryId && x.Movies.Any(y => y.Id == movieId));
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
