using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities;
using System.Collections.Generic;

namespace MovieScrapper.Business
{
    public class WatchedMovieService: IWatchedMovieService
    {

        private readonly IWatchedMovieRepository _watchedMovieRepository;


        public WatchedMovieService(IWatchedMovieRepository watchedMovieRepository)
        {
            _watchedMovieRepository = watchedMovieRepository;
        }

        public Watched AddWatchedEntity(Watched watchedEntity)
        {
            var repo = new WatchedMovieRepository();
            return repo.AddWatchedEntity(watchedEntity);
        }

        public IEnumerable<Watched> GetAllUsersWatchedAMovie()
        {
            var repo = new WatchedMovieRepository();
            return repo.GetAllUsersWatchedAMovie();
        }

        public IEnumerable<Watched> GetAllWatchedMovies(string userId)
        {
            var repo = new WatchedMovieRepository();
            return repo.GetAllWatchedMovies(userId);
        }

        public Watched GetUserWatchedEntity(string userId)
        {
            var repo = new WatchedMovieRepository();
            return repo.GetUserWatchedEntity(userId);
        }
    }
}
