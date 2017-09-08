using MovieScrapper.Entities;
using System.Collections.Generic;

namespace MovieScrapper.Data.Interfaces
{
    public interface IBetRepository
    {
        IEnumerable<Bet> GetAllUserBets(string userId);

        Bet GetUserBetEntity(string userId);

        Bet MakeBetEntity(string userId, int movieId, int categoryId);
        
    }
}
