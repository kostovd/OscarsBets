using MovieScrapper.Entities.StatisticsModels;
using System.Collections.Generic;

namespace MovieScrapper.Data.Interfaces
{
    public interface IViewModelsRepository
    {
        List<WatchedMovies> GetWatchedMoviesData();

        List<BetsStatistic> GetBetsData();

        List<Winners> GetWinner();
        
    }
}
