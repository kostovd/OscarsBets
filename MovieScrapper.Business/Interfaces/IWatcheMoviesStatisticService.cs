using System.Collections.Generic;

namespace MovieScrapper.Business.Interfaces
{
    public interface IWatcheMoviesStatisticService
    {
        Dictionary<string, List<string>> GetData();

        string[] GetTitles();

        string[] GetUsers();
    }
}
