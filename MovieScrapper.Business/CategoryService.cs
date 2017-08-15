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
        #region ADD
        public void AddCategory(Category category)
        {
            var repo = new CategoryRepository();
            repo.AddCategory(category);
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

        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            var repo = new CategoryRepository();
            return repo.AddWatchedEntity(watchedEntity);
        }

        #endregion

        public void ChangeMovieStatus(string userId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.ChangeMovieStatus(userId, movieId);
        }

        public void DeleteCategory(int id)
        {
            var repo = new CategoryRepository();
            repo.DeleteCategory(id);
        }

        

        public void EditCategory(Category category)
        {
            var repo = new CategoryRepository();
            repo.EditCategory(category);
        }

        #region GET

        public IEnumerable<Category> GetAll()
        {
            var repo = new CategoryRepository();
            return repo.GetAll();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var repo = new CategoryRepository();
            return repo.GetAllMovies();
        }

        public IEnumerable<Movie> GetAllMoviesInCategory(int categoryId)
        {
            var repo = new CategoryRepository();
            return repo.GetAllMoviesInCategory(categoryId);
        }

        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            var repo = new CategoryRepository();
            return repo.GetAllUserBets(userId);
        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)

        {
            var repo = new CategoryRepository();
            return repo.GetAllWatchedMovies(userId);
        }
        
        public Category GetCategory(int id)
        {
            var repo = new CategoryRepository();
            return repo.GetCategory(id);
        }
        public Movie GetMovie(int id)
        {
            var repo = new CategoryRepository();
            return repo.GetMovie(id);
        }

        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            return repo.GetMovieInCategory(categoryId, movieId);
        }

        public bool IsGameStopped()
        {
            var repo = new CategoryRepository();

            int result = DateTime.Compare( repo.GetStopDate().StopGameDate, DateTime.Now);
            if (result<0)
            {
                return true;
            }

            else
            {
                return false;
            }
            
        }

        public Bet GetUserBetEntity(string userId)
        {
            var repo = new CategoryRepository();
            return repo.GetUserBetEntity(userId);
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            var repo = new CategoryRepository();
            return repo.GetUserWatchedEntity(userId);
        }

        #endregion

        public Bet MakeBetEntity(string userId, int movieId, int categoryId)
        {
            var repo = new CategoryRepository();
            return repo.MakeBetEntity(userId, movieId, categoryId);
        }

        public void MarkAsWinner(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.MarkAsWinner(categoryId, movieId);
        }

        public void RemoveMovieFromCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.RemoveMovieFromCategory(categoryId, movieId);
        }                                

    }
}
