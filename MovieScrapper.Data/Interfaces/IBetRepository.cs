using MovieScrapper.Entities;
using System.Collections.Generic;

namespace MovieScrapper.Data.Interfaces
{
    public interface IBetRepository
    {
        IEnumerable<Bet> GetAllUserBets(string userId);

        IEnumerable<Bet> GetAllBetsByCategory(int categoryId);

        void MakeBetEntity(string userId, int nominationId);
    }
}
