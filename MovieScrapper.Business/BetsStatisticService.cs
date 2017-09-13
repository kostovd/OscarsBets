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

        //public Dictionary<string, List<string[]>> GetData1()
        //{
        //    List<BetsStatistic> bets = _viewModelsRepository.GetBetsData();           
        //    var stringArrEmails = bets.Select(b=>b.Email).Where(e => e != null).ToArray();
        //    var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();

        //    var winningMovieCategories = GetWinningCategoryMovies();
                 
        //    Dictionary<string, List<string[]>> myDict = new Dictionary<string, List<string[]>>();

        //    foreach (var email in collectionWithDistinctEmails)
        //    {

        //        string currentCategory;
        //        string currentTitle;

        //        List<string[]> userCategories = new List<string[]>();

        //        for (int j = 0; j < bets.Count; j++)
        //        {
        //            string[] categoryMovie = new string[3];
        //            var bet = bets[j];
        //            if (bet.Email.ToString() == email)
        //            {
        //                currentCategory = bet.CategoryTitle.ToString();
        //                currentTitle = bet.MovieTitle.ToString();

        //                categoryMovie[0] = currentCategory;
        //                categoryMovie[1] = currentTitle;
        //                var counter = 0;
        //                foreach(var winningMovieCategory in winningMovieCategories)
        //                {
        //                    var currentWinningCategory = winningMovieCategory[0];
        //                    var currentWinningMovie = winningMovieCategory[1];
        //                    if(currentCategory== currentWinningCategory)
        //                    {
        //                        if(currentTitle== currentWinningMovie)
        //                        {
        //                            counter++;
        //                        }
        //                    }
        //                }
        //                categoryMovie[2] = counter.ToString();
        //                userCategories.Add(categoryMovie);

        //            }
        //        }

        //        myDict.Add(email, userCategories);
        //    }
                    
        //    myDict = myDict.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);           
        //    return myDict;

        //}

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
            return string.Empty;

            //var dict = GetData();
            //var arrayOfAllKeys = dict.Keys.ToArray();
            //var bestCount = 0;
            //List<string[]> winners = new List<string[]>();
            //foreach (var userName in arrayOfAllKeys)
            //{
            //    var userCategoriesMovies = dict[userName];
            //    var currentUserCount = 0;
            //    foreach (var entity in userCategoriesMovies)
            //    {
            //        if (entity[2] == 1.ToString())
            //        {
            //            currentUserCount++;
            //        }
            //    }
            //    if (currentUserCount == bestCount)
            //    {
            //        bestCount = currentUserCount;
            //        var winnerSoFar = userName;
            //        string[] winningEntity = new string[2];
            //        winningEntity[0] = winnerSoFar;
            //        winningEntity[1] = bestCount.ToString();
            //        winners.Add(winningEntity);

            //    }

            //    else if (currentUserCount > bestCount)
            //    {
            //        bestCount = currentUserCount;
            //        var winnerSoFar = userName;
            //        string[] winningEntity = new string[2];
            //        winningEntity[0] = winnerSoFar;
            //        winningEntity[1] = bestCount.ToString();
            //        winners.Clear();
            //        winners.Add(winningEntity);

            //    }


            //}
            //if (winners.Count == 0)
            //{
            //    return "<div class='redBorder'>There are no winners</div>";
            //}
            //else if (winners.Count == 1)
            //{
            //    var winner = winners[0];
            //    return "<div class='redBorder'>The winner is " + winner[0] + "! He/she guessed right in " + winner[1] + " categories!</div>";
            //}
            //else
            //{

            //    var finalWinners = new List<string[]>();
            //    var watchedMoviesService = new WatcheMoviesStatisticService(_viewModelsRepository);
            //    var usersMovies = watchedMoviesService.GetData();
            //    var arrayOfAllUsersWatchedMovies = usersMovies.Keys.ToArray();
            //    var bestMovieCount = 0;
            //    foreach (var currentWinner in winners)
            //    {
            //        foreach (var user in arrayOfAllUsersWatchedMovies)
            //        {

            //            if (currentWinner[0] == user)
            //            {
            //                var currentUserWatchedMovies = usersMovies[user];
            //                if (currentUserWatchedMovies.Count == bestMovieCount)
            //                {
            //                    string[] currentEntity = new string[2];
            //                    currentEntity[0] = user;
            //                    currentEntity[1] = bestMovieCount.ToString();
            //                    finalWinners.Add(currentEntity);
            //                }
            //                else if (currentUserWatchedMovies.Count > bestMovieCount)
            //                {
            //                    string[] currentEntity = new string[2];
            //                    bestMovieCount = currentUserWatchedMovies.Count;
            //                    currentEntity[0] = user;
            //                    currentEntity[1] = bestMovieCount.ToString();
            //                    finalWinners.Clear();
            //                    finalWinners.Add(currentEntity);
            //                }
            //            }
            //        }
            //    }
            //    if (finalWinners.Count == 0)
            //    {
            //        string resault = "<div class='redBorder'>The winners are: <br/>";
            //        foreach (var winner in winners)
            //        {
            //            resault += winner[0] + "<br/>";
            //        }
            //        resault += "They guessed rigth in " + winners[0][1] + " categories!</div>";
            //        return resault;
            //    }
            //    else if (finalWinners.Count == 1)
            //    {
            //        var resault = "<div class='redBorder'>" + finalWinners[0][0] + " wins! He/she guessed right in " + winners[0][1] + " categories and watched " + finalWinners[0][1] + " movies!</div>";
            //        return resault;
            //    }
            //    else
            //    {
            //        string resault = "<div class='redBorder'>The winners are: <br/>";
            //        foreach (var winner in finalWinners)
            //        {
            //            resault += winner[0] + "<br/>";
            //        }
            //        resault += "They watched " + finalWinners[0][1] + " movies and guessed rigth in " + winners[0][1] + " categories!</div>";
            //        return resault;
            //    }

            //}
        }

        //private List<string[]> GetWinningCategoryMovies()
        //{          
        //    List<Winners> winners = _viewModelsRepository.GetWinner();            
        //    //var winningCategories = winners.Select(w => w.Category).Where(x => x != null).ToArray();
        //    List <string[]> winningMovies = new List<string[]>();     
        //    string currentCategory;
        //    string currentWinner;
               
        //        for (int i = 0; i < winners.Count; i++)
        //        {
        //            string[] categoryMovie = new string[2];
        //            var winner = winners[i];
        //            currentCategory = winner.Category.ToString();
        //            currentWinner = winner.Winner.ToString();
        //            categoryMovie[0] = currentCategory;
        //            categoryMovie[1] = currentWinner;

        //            winningMovies.Add(categoryMovie);
        //        }         

        //    return winningMovies;
        //}

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

        //public List<string[]> GetWinners()
        //{
        //    var dt = _viewModelsRepository.GetWinner();
          
        //    string currentCategory;
        //    string currentWinner;
        //    List<string[]> winners = new List<string[]>();
        //    for (int j = 0; j < dt.Count; j++)
        //    {
        //        string[] categoryWinner = new string[2];
        //        var row = dt[j];

        //        currentCategory = row.Category.ToString();
        //        currentWinner = row.Winner.ToString();

        //        categoryWinner[0] = currentCategory;
        //        categoryWinner[1] = currentWinner;

        //        winners.Add(categoryWinner);
        //    }
        //    return winners;
        //}
        
    }
}
