using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class CategoryService
    {
        public IEnumerable<MovieCategory> GetAll()
        {
            var repo = new CategoryRepository();
            return repo.GetAll();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var repo = new CategoryRepository();
            return repo.GetAllMovies();

        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)

        {
            var repo = new CategoryRepository();
            return repo.GetAllWatchedMovies(userId);
        }

        public IEnumerable<Movie> GetAllMoviesInCategory(int categoryId)
        {
            var repo = new CategoryRepository();
            return repo.GetAllMoviesInCategory(categoryId);
        }
        public MovieCategory GetCategory(int id)
        {
            var repo = new CategoryRepository();
            return repo.GetCategory(id);
        }
        public Movie GetMovie(int id)
        {
            var repo = new CategoryRepository();
            return repo.GetMovie(id);
        }
        public void AddCategory(MovieCategory category)
        {
            var repo = new CategoryRepository();
            repo.AddCategory(category);
        }
        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            var repo = new CategoryRepository();
            return repo.AddWatchedEntity(watchedEntity);

        }
        public void EditCategory(MovieCategory category)
        {
            var repo = new CategoryRepository();
            repo.EditCategory(category);
        }

        public void AddMovie(Movie movie)
        {
            var repo = new CategoryRepository();
            repo.AddMovie(movie);
        }
        public void AddMovieInCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.AddMovieInCategory(categoryId, movieId);
        }
        //public void AddWatchedMovie(Watched watchedEntity, int movieId)
        //{
        //    var repo = new CategoryRepository();
        //    repo.AddWatchedMovie(watchedEntity, movieId);

        //}
        public void AddWatchedMovie(string userId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.AddWatchedMovie(userId, movieId);

        }
        public void DeleteCategory(int id)
        {
            var repo = new CategoryRepository();
            repo.DeleteCategory(id);
        }

        public void RemoveMovieFromCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.RemoveMovieFromCategory(categoryId, movieId);
        }

        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            return repo.GetMovieInCategory(categoryId, movieId);
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            var repo = new CategoryRepository();
            return repo.GetUserWatchedEntity(userId);
        }

    }
}
