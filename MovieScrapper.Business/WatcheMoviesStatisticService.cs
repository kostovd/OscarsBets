using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities.StatisticsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MovieScrapper.Business
{
    public class WatcheMoviesStatisticService: IWatcheMoviesStatisticService
    {
        private readonly IViewModelsRepository _viewModelsRepository;


        public WatcheMoviesStatisticService(IViewModelsRepository viewModelsRepository)
        {
            _viewModelsRepository = viewModelsRepository;
        }

        public List<WatchedObject> GetData()
        {           
            List<WatchedMovies> watchedEntities = _viewModelsRepository.GetWatchedMoviesData();

            Dictionary<string, List<string>> watchedDict = new Dictionary<string, List<string>>();

            foreach (var watchedEntity in watchedEntities)
            {
                if (watchedEntity.Email != null)
                {                   
                    List<string> watchedMoviesList;
                    if (!watchedDict.TryGetValue(watchedEntity.Email, out watchedMoviesList))
                    {
                        watchedMoviesList = new List<string>();
                        watchedDict.Add(watchedEntity.Email, watchedMoviesList);
                    }

                    watchedMoviesList.Add(watchedEntity.Title);
                }
            }

            return watchedDict.Select(x => new WatchedObject { UserEmail = x.Key, MovieTitles = x.Value }).ToList();       
        }

        //public Dictionary<string, List<string>> GetData1()
        //{
        //    var watchedMovies = _viewModelsRepository.GetWatchedMoviesData();
        //    var stringArrTitles = watchedMovies.Select(m => m.Title).ToArray();
        //    var collectionWithAllDistinctTitles = stringArrTitles.Distinct().ToArray();            
        //    int allTitlesCount = collectionWithAllDistinctTitles.Count();
        //    var stringArrEmails = watchedMovies.Select(m => m.Email).Where(e => e != null).ToArray();
        //    var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();
        //    int emailsCount = collectionWithDistinctEmails.Count();         

        //    Dictionary<string, List<string>> myDict = new Dictionary<string, List<string>>();
          
        //    foreach(var email in collectionWithDistinctEmails)
        //    {               
        //        List<string> userTitles= new List<string>();
        //        for (int j =0; j< allTitlesCount; j++)
        //        {
                   
        //            string currentTitle;
        //            foreach (var item in watchedMovies)
        //            {
        //                if (item.Email != null)
        //                {
        //                    if (item.Email.ToString() == email)
        //                    {
        //                        currentTitle = item.Title.ToString();
        //                        userTitles.Add(currentTitle);
        //                    }
        //                }
        //            }
                   
        //        }
        //        List<string> distinctTitles = userTitles.Distinct().ToList();
        //        myDict.Add(email, distinctTitles);
        //    }
        //    myDict = myDict.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
        //    return myDict;
        //}

        public string [] GetTitles()
        {
            var watchedMovies = _viewModelsRepository.GetWatchedMoviesData();
            var stringArrTitles =  watchedMovies.Select(m => m.Title).ToArray();
            var collectionWithDistinctTitles = stringArrTitles.Distinct().ToArray();
            int titlesCount = collectionWithDistinctTitles.Count();                                             
            return collectionWithDistinctTitles;
        }

        public string[] GetUsers()
        {
            var watchedMovies = _viewModelsRepository.GetWatchedMoviesData();
            var stringArrEmails = watchedMovies.Select(m=>m.Email).Where(e=>e!=null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();

            return collectionWithDistinctEmails;
        }

    }
}

