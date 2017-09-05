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
    public class CategoryService
    {
        #region ADD
        public void AddCategory(Category category)
        {
            var repo = new CategoryRepository();
            repo.AddCategory(category);
        }
      

        public void AddMovieInCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.AddMovieInCategory(categoryId, movieId);
        }

        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            var repo = new WatchedMovieRepository();
            return repo.AddWatchedEntity(watchedEntity);
        }

        #endregion
        public bool AreWinnersSet()
        {
            var repo = new CategoryRepository();
            return repo.AreWinnersSet();
        }
        public void ChangeGameStartDate(DateTime stopDate)
        {
            var repo = new GamePropertyRepository();
            repo.ChangeGameStartDate(stopDate);
        }
        public void ChangeGameStopDate(DateTime stopDate)
        {
            var repo = new GamePropertyRepository();
            repo.ChangeGameStopDate(stopDate);
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

                

        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            var repo = new BetRepository();
            return repo.GetAllUserBets(userId);
        }

        public IEnumerable<Watched> GetAllUsersWatchedAMovie()
        {
            var repo = new WatchedMovieRepository();
            return repo.GetAllUsersWatchedAMovie();
        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)

        {
            var repo = new WatchedMovieRepository();
            return repo.GetAllWatchedMovies(userId);
        }
        
        public Category GetCategory(int id)
        {
            var repo = new CategoryRepository();
            return repo.GetCategory(id);
        }

        public DateTime GetGameStartDate()
        {
            var repo = new GamePropertyRepository();
            return repo.GetGameStartDate();
        }

        public DateTime GetGameStopDate()
        {
            var repo = new GamePropertyRepository();
            return repo.GetGameStopDate();
        }
       

        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            return repo.GetMovieInCategory(categoryId, movieId);
        }

        public bool IsGameStopped()
        {
            var repo = new GamePropertyRepository();

            GameProperties stopDateObject = repo.GetDate();
            DateTime stopDate = (stopDateObject != null ? stopDateObject.StopGameDate : DateTime.Now);
            return (stopDate < DateTime.Now);        
           
        }

        public bool IsGameNotStartedYet()
        {
            var repo = new GamePropertyRepository();

            GameProperties dateObject = repo.GetDate();
            DateTime startDate = (dateObject != null ? dateObject.StartGameDate : DateTime.Now);
            return (startDate > DateTime.Now);

        }

        public Bet GetUserBetEntity(string userId)
        {
            var repo = new BetRepository();
            return repo.GetUserBetEntity(userId);
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            var repo = new WatchedMovieRepository();
            return repo.GetUserWatchedEntity(userId);
        }

        #endregion

        public Bet MakeBetEntity(string userId, int movieId, int categoryId)
        {
            var repo = new BetRepository();
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
