using MovieScrapper.Data;
using MovieScrapper.Entities.StatisticsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class BetsStatisticService
    {
        public Dictionary<string, List<string[]>> GetData()
        {
            var data = new ViewModelsRepository();
            List<BetsStatistic> bets = data.GetBetsData();
            // var stringArrEmails = dt.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var stringArrEmails = bets.Select(b=>b.Email).Where(e => e != null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();

            var winningMovieCategories = GetWinningCategoryMovies();
                 
            Dictionary<string, List<string[]>> myDict = new Dictionary<string, List<string[]>>();

            foreach (var email in collectionWithDistinctEmails)
            {

                string currentCategory;
                string currentTitle;

                List<string[]> userCategories = new List<string[]>();

                for (int j = 0; j < bets.Count; j++)
                {
                    string[] categoryMovie = new string[3];
                    var bet = bets[j];
                    if (bet.Email.ToString() == email)
                    {
                        currentCategory = bet.Category.ToString();
                        currentTitle = bet.Title.ToString();

                        categoryMovie[0] = currentCategory;
                        categoryMovie[1] = currentTitle;
                        var counter = 0;
                        foreach(var winningMovieCategory in winningMovieCategories)
                        {
                            var currentWinningCategory = winningMovieCategory[0];
                            var currentWinningMovie = winningMovieCategory[1];
                            if(currentCategory== currentWinningCategory)
                            {
                                if(currentTitle== currentWinningMovie)
                                {
                                    counter++;
                                }
                            }
                        }
                        categoryMovie[2] = counter.ToString();
                        userCategories.Add(categoryMovie);

                    }
                }

                myDict.Add(email, userCategories);
            }
                    
            myDict = myDict.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);           
            return myDict;

        }

        public string GetWinner()
        {
            var dict = GetData();
            var arrayOfAllKeys = dict.Keys.ToArray();
            var bestCount = 0;
            List<string[]> winners=new List<string[]>();
            foreach (var userName in arrayOfAllKeys)
            {
                var userCategoriesMovies = dict[userName];
                var currentUserCount = 0;
                foreach(var entity in userCategoriesMovies)
                {
                    if (entity[2] == 1.ToString())
                    {
                        currentUserCount++;
                    }
                }
                if (currentUserCount == bestCount)
                {
                    bestCount = currentUserCount;
                    var winnerSoFar = userName;
                    string[] winningEntity = new string[2];
                    winningEntity[0] = winnerSoFar;
                    winningEntity[1] = bestCount.ToString();
                    winners.Add(winningEntity);

                }

                else if (currentUserCount > bestCount)
                {
                    bestCount = currentUserCount;
                    var winnerSoFar = userName;
                    string[] winningEntity=new string[2];
                    winningEntity[0] = winnerSoFar ;
                    winningEntity[1] = bestCount.ToString();
                    winners.Clear();
                    winners.Add(winningEntity);
                                       
                }

                
            }
            if (winners.Count == 0)
            {
                return "<div class='redBorder'>There are no winners</div>";
            }
            else if (winners.Count == 1)
            {
                var winner = winners[0];
                return "<div class='redBorder'>The winner is " + winner[0] + "! He/she guessed right in " + winner[1] + " categories!</div>";
            }
            else
            {
                var finalWinners = new List<string[]>();
                var watchedMoviesService = new WatcheMoviesStatisticService();
                var usersMovies = watchedMoviesService.GetData();
                var arrayOfAllUsersWatchedMovies = usersMovies.Keys.ToArray();
                var bestMovieCount = 0;
                foreach(var currentWinner in winners )
                {                 
                    foreach(var user in arrayOfAllUsersWatchedMovies)
                    {
                        
                        if (currentWinner[0]==user)
                        {
                            var currentUserWatchedMovies = usersMovies[user];
                            if (currentUserWatchedMovies.Count == bestMovieCount)
                            {
                                string[] currentEntity=new string[2];
                                currentEntity[0] = user;
                                currentEntity[1] = bestMovieCount.ToString();
                                finalWinners.Add(currentEntity);
                            }
                            else if (currentUserWatchedMovies.Count > bestMovieCount)
                            {
                                string[] currentEntity = new string[2];
                                bestMovieCount = currentUserWatchedMovies.Count;
                                currentEntity[0] = user;
                                currentEntity[1] = bestMovieCount.ToString();                               
                                finalWinners.Clear();
                                finalWinners.Add(currentEntity);
                            }
                        }
                    }
                }
                if (finalWinners.Count == 0)
                {
                    string resault = "<div class='redBorder'>The winners are: <br/>";
                    foreach (var winner in winners)
                    {
                        resault += winner[0] + "<br/>";
                    }
                    resault += "They guessed rigth in " + winners[0][1] + " categories!</div>";
                    return resault;
                }
                else if(finalWinners.Count == 1)
                {
                    var resault = "<div class='redBorder'>"+finalWinners[0][0] +" wins! He/she guessed right in "+ winners[0][1]+ " categories and watched "+ finalWinners[0][1]+ " movies!</div>";
                    return resault;
                }
                else
                {
                    string resault = "<div class='redBorder'>The winners are: <br/>";
                    foreach (var winner in finalWinners)
                    {
                        resault += winner[0] + "<br/>";
                    }
                    resault += "They watched " + finalWinners[0][1] + " movies and guessed rigth in "+ winners[0][1]+ " categories!</div>";
                    return resault;
                }

            }
        }

        private List<string[]> GetWinningCategoryMovies()
        {
            var data = new ViewModelsRepository();
            List<Winners> winners = data.GetWinner();
            //var winningCategories = dt.AsEnumerable().Select(r => r.Field<string>("Category")).Where(x => x != null).ToArray();
            var winningCategories = winners.Select(w => w.Category).Where(x => x != null).ToArray();

            List <string[]> winningMovies = new List<string[]>();
           
                string currentCategory;
                string currentWinner;
               
                for (int i = 0; i < winners.Count; i++)
                {
                    string[] categoryMovie = new string[2];
                    var winner = winners[i];
                    currentCategory = winner.Category.ToString();
                    currentWinner = winner.Winner.ToString();
                    categoryMovie[0] = currentCategory;
                    categoryMovie[1] = currentWinner;

                    winningMovies.Add(categoryMovie);
                }
            

            return winningMovies;
        }

        public string[] GetCategories()
        {
            var data = new ViewModelsRepository();
            var dt = data.GetBetsData();

            //var stringArrCategories = dt.AsEnumerable().Select(r => r.Field<string>("Category")).ToArray();
            var stringArrCategories = dt.Select(x => x.Category).ToArray();
            var collectionWithDistinctCategories = stringArrCategories.Distinct().ToArray();
            return collectionWithDistinctCategories;
        }

        public List<string[]> GetWinners()
        {
            var data = new ViewModelsRepository();
            var dt = data.GetWinner();
          
            string currentCategory;
            string currentWinner;
            List<string[]> winners = new List<string[]>();
            for (int j = 0; j < dt.Count; j++)
            {
                string[] categoryWinner = new string[2];
                var row = dt[j];

                currentCategory = row.Category.ToString();
                currentWinner = row.Winner.ToString();

                categoryWinner[0] = currentCategory;
                categoryWinner[1] = currentWinner;

                winners.Add(categoryWinner);
            }
            return winners;
        }
    }
}
