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

        public void MakeBetEntity(string userId, int nominationId)
        {
            using (var ctx = new MovieContext())
            {
                var selectedNomination = ctx.Nominations
                    .Include(x => x.Category)
                    .Where(x => x.Id == nominationId)
                    .SingleOrDefault();

                Bet categoryUserBet = ctx.Bets
                    .Include(x => x.Nomination)
                    .Where(x => x.UserId == userId)
                    .Where(x => x.Nomination.Category.Id == selectedNomination.Category.Id)
                    .FirstOrDefault();

                if (categoryUserBet == null)
                {
                    categoryUserBet = new Bet()
                    {
                        UserId = userId,
                        Nomination = selectedNomination,
                    };

                    ctx.Entry(categoryUserBet).State = EntityState.Added;
                }
                else if (categoryUserBet.Nomination.Id == selectedNomination.Id)
                {
                    ctx.Entry(categoryUserBet).State = EntityState.Deleted;
                }
                else
                {
                    categoryUserBet.Nomination = selectedNomination;
                    ctx.Entry(categoryUserBet).State = EntityState.Modified;
                }

                ctx.SaveChanges();
            }
        }
    }
}
