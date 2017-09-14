using System.Collections.Generic;

namespace MovieScrapper.Business
{
    public class WatchedObject
    {
        public string UserEmail { get; set; }
        public List<string> MovieTitles { get; set; }
    }
}
