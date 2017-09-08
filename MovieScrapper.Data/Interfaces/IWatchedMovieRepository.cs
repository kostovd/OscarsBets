using MovieScrapper.Entities;
using System.Collections.Generic;

namespace MovieScrapper.Data.Interfaces
{
    public interface IWatchedMovieRepository
    {
        Watched AddWatchedEntity(Watched watchedEntity);

        IEnumerable<Watched> GetAllUsersWatchedAMovie();

        IEnumerable<Watched> GetAllWatchedMovies(string userId);

        Watched GetUserWatchedEntity(string userId);        
    }
}
