using System.Collections.Generic;

namespace MovieScrapper.Business.Interfaces
{
    interface IWatcheMoviesStatisticService
    {
        Dictionary<string, List<string>> GetData();

        string[] GetTitles();

        string[] GetUsers();       
    }
}
