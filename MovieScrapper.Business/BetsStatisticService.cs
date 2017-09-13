using Microsoft.Practices.Unity;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities.StatisticsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class BetsStatisticService: IBetStatisticService
    {
        private readonly IViewModelsRepository _viewModelsRepository;


        public BetsStatisticService(IViewModelsRepository viewModelsRepository)
        {
            _viewModelsRepository = viewModelsRepository;
        }       

        public List<BetObject> GetData()
        {
            List<Winners> winners = _viewModelsRepository.GetWinner();

            Dictionary<string, List<BetMovieObject>> betsDict = new Dictionary<string, List<BetMovieObject>>();

            List<BetsStatistic> bets = _viewModelsRepository.GetBetsData();

            foreach (var bet in bets)
            {
                if (bet.Email != null)
                {
                    BetMovieObject betMovieObject = new BetMovieObject
                    {
                        CategoryTitle = bet.CategoryTitle,
                        MovieTitle = bet.MovieTitle,
                        IsRightGuess = winners.Any(x => x.Category == bet.CategoryTitle && x.Winner == bet.MovieTitle),
                    };

                    List<BetMovieObject> betMovieObjectList;
                    if (!betsDict.TryGetValue(bet.Email, out betMovieObjectList))
                    {
                        betMovieObjectList = new List<BetMovieObject>();
                        betsDict.Add(bet.Email, betMovieObjectList);
                    }

                    betMovieObjectList.Add(betMovieObject);
                }
            }

            return betsDict.Select(x => new BetObject { UserEmail = x.Key, UserBets = x.Value }).ToList();
        }

        public string GetWinner()
        {
            var users = GetData();            
            List<Winners> winners = _viewModelsRepository.GetWinner();
            string currentWinnerName = String.Empty;
            int currentWinnerScores = 0;

            foreach (var user in users)
            {                
                int scores = 0;
                foreach (var bet in user.UserBets)
                {
                    if (bet.IsRightGuess)
                    {
                        scores++;
                    }                  
                }
                if (scores > currentWinnerScores)
                {
                    currentWinnerScores = scores;
                    currentWinnerName = user.UserEmail;
                }
            }

            var resault= "The winner is "+ currentWinnerName +"! He/She guessed right in "+ currentWinnerScores.ToString()+" categories!";
            return resault;

        }

        public string[] GetCategories()
        {
            var dt = _viewModelsRepository.GetBetsData();
            var stringArrCategories = dt.Select(x => x.CategoryTitle).ToArray();
            var collectionWithDistinctCategories = stringArrCategories.Distinct().ToArray();
            return collectionWithDistinctCategories;
        }

        public List<Winners> GetWinners()
        {
            List<Winners> winners = _viewModelsRepository.GetWinner();
            return winners;
        }      

    }
}
