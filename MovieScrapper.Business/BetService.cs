using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;

namespace MovieScrapper.Business
{
    public class BetService: IBetService
    {
        private readonly IBetRepository _betRepository;

        public BetService(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public IEnumerable<Bet> GetAllUserBets(string userId)
        {
            return _betRepository.GetAllUserBets(userId);
        }

        public Bet MakeBetEntity(string userId, int nominationId)
        {
            return _betRepository.MakeBetEntity(userId, nominationId);
        }
    }

}
