using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities.StatisticsModels;
using System.Collections.Generic;
using System.Linq;

namespace MovieScrapper.Data
{
    public class ViewModelsRepository: IViewModelsRepository
    {

        public List<WatchedMovies> GetWatchedMoviesData()
        {
            string query = "SELECT Movies.Id, Movies.Title, AspNetUsers.Email FROM Movies LEFT JOIN WatchedMovies on Movies.Id = WatchedMovies.Movie_Id LEFT JOIN AspNetUsers on WatchedMovies.Watched_UserId = AspNetUsers.Id";
            using (var ctx = new MovieContext())
            {
                List<WatchedMovies> resaults = ctx.Database.SqlQuery<WatchedMovies>(query).ToList();
                return resaults;
            }
        }

        public List<BetsStatistic> GetBetsData()
        {
            string query = "SELECT Bets.Id, AspNetUsers.Email, Categories.CategoryTtle as CategoryTitle, Movies.Title as MovieTitle FROM Bets INNER JOIN Movies ON Bets.Movie_Id = Movies.Id INNER JOIN AspNetUsers ON Bets.UserId = AspNetUsers.Id JOIN Categories ON Bets.Category_Id = Categories.Id";
            using (var ctx = new MovieContext())
            {
                List<BetsStatistic> resaults = ctx.Database.SqlQuery<BetsStatistic>(query).ToList();
                return resaults;
            }
        }


        public List<Winners> GetWinner()
        {
            string query = "SELECT Categories.CategoryTtle as Category, Movies.Title as Winner FROM Categories INNER JOIN Movies ON Categories.Winner_Id = Movies.Id";
            using (var ctx = new MovieContext())
            {
                List<Winners> resaults = ctx.Database.SqlQuery<Winners>(query).ToList();
                return resaults;
            }
        }
    }
}
