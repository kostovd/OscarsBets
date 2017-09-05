using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Data
{
    public class BetRepository
    {
        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var bets = ctx.Bets.Where(bet => bet.UserId == userId).ToList();
                return bets;
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
    }
}
