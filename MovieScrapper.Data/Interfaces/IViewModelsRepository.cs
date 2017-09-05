using MovieScrapper.Entities.StatisticsModels;
using System.Collections.Generic;

namespace MovieScrapper.Data.Interfaces
{
    interface IViewModelsRepository
    {
        List<WatchedMovies> GetWatchedMoviesData();

        List<BetsStatistic> GetBetsData();

        List<Winners> GetWinner();
        
    }
}
