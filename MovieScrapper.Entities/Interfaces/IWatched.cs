using System.Collections.Generic;

namespace MovieScrapper.Entities.Interfaces
{
    interface IWatched
    {
        string UserId { get; set; }
        ICollection<Movie> Movies { get; set; }
    }
}
