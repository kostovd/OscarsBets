using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;

namespace MovieScrapper.Business
{
    public class BetService: IBetService
    {
        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            var repo = new BetRepository();
            return repo.GetAllUserBets(userId);
        }

        public Bet GetUserBetEntity(string userId)
        {
            var repo = new BetRepository();
            return repo.GetUserBetEntity(userId);
        }

        public Bet MakeBetEntity(string userId, int movieId, int categoryId)
        {
            var repo = new BetRepository();
            return repo.MakeBetEntity(userId, movieId, categoryId);
        }
    }

}
