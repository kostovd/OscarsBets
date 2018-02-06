using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MovieScrapper.Data
{
    public class BetRepository: IBetRepository
    {
        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            using (var ctx = new MovieContext())
            {
                var bets = ctx.Bets.Where(bet => bet.UserId == userId).ToList();
                return bets;
            }
        }

        public Bet MakeBetEntity(string userId, int nominationId)
        {
            using (var ctx = new MovieContext())
            {
                var selectedNomination = ctx.Nominations
                    .Include(x => x.Category)
                    .Where(x => x.Id == nominationId)
                    .SingleOrDefault();

                Bet categoryUserBet = ctx.Bets
                    .Where(x => x.UserId == userId)
                    .Where(x => x.Nomination.Category == selectedNomination.Category)
                    .FirstOrDefault();

                if (categoryUserBet != null)
                {
                    categoryUserBet.Nomination = selectedNomination;
                }
                else
                {
                    categoryUserBet = new Bet()
                    {
                        UserId = userId,
                        Nomination = selectedNomination,
                    };

                    categoryUserBet = ctx.Bets.Add(categoryUserBet);
                }

                ctx.SaveChanges();
                return categoryUserBet;
            }
        }
    }
}
