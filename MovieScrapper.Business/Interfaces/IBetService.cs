using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business.Interfaces
{
    public interface IBetService
    {
        IEnumerable<Bet> GetAllUserBets(string userId);

        void MakeBetEntity(string userId, int nominationId);       
    }
}
