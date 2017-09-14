using System.Collections.Generic;

namespace MovieScrapper.Business.Interfaces
{
    public interface IWatcheMoviesStatisticService
    {
        List<WatchedObject> GetData();

        string[] GetTitles();

        string[] GetUsers();
    }
}
