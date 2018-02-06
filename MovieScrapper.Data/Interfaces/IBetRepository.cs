using MovieScrapper.Entities;
using System.Collections.Generic;

namespace MovieScrapper.Data.Interfaces
{
    public interface IBetRepository
    {
        IEnumerable<Bet> GetAllUserBets(string userId);

        Bet MakeBetEntity(string userId, int nominationId);
    }
}
