using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MovieScrapper.Business
{
    public class WatcheMoviesStatisticService
    {

       

        public Dictionary<string, List<string>> GetData()
        {
            var data = new ViewModelsRepository();
            var watchedMovies = data.GetWatchedMoviesData();

            //var stringArrTitles= recievedTable.AsEnumerable().Select(r => r.Field<string>("Title")).ToArray();
            var stringArrTitles = watchedMovies.Select(m => m.Title).ToArray();
            var collectionWithAllDistinctTitles = stringArrTitles.Distinct().ToArray();            
            int allTitlesCount = collectionWithAllDistinctTitles.Count();
            //var stringArrEmails = recievedTable.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var stringArrEmails = watchedMovies.Select(m => m.Email).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();
            int emailsCount = collectionWithDistinctEmails.Count();

            

            Dictionary<string, List<string>> myDict = new Dictionary<string, List<string>>();
          
            //for (int i = 0; i < emailsCount; i++)
            foreach(var email in collectionWithDistinctEmails)
            {               
                List<string> userTitles= new List<string>();
                for (int j =0; j< allTitlesCount; j++)
                {
                   
                    string currentTitle;
                    foreach (var item in watchedMovies)
                    {
                        if (item.Email.ToString() == email )
                        {
                            currentTitle= item.Title.ToString();
                            userTitles.Add(currentTitle);
                        }
                    }
                   
                }
                List<string> distinctTitles = userTitles.Distinct().ToList();
                myDict.Add(email, distinctTitles);
            }
            myDict = myDict.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            return myDict;
        }



        public string [] GetTitles()
        {
            var data = new ViewModelsRepository();
            var watchedMovies = data.GetWatchedMoviesData();

            var stringArrTitles =  watchedMovies.Select(m => m.Title).ToArray();
            var collectionWithDistinctTitles = stringArrTitles.Distinct().ToArray();
            int titlesCount = collectionWithDistinctTitles.Count();                                             
            return collectionWithDistinctTitles;
        }

        public string[] GetUsers()
        {
            var data = new ViewModelsRepository();
            var watchedMovies = data.GetWatchedMoviesData();
            // var stringArrEmails = recievedTable.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var stringArrEmails = watchedMovies.Select(m=>m.Email).Where(e=>e!=null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();

            return collectionWithDistinctEmails;
        }

    }
}

