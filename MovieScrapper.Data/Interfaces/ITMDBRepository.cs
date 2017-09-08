using MovieScrapper.Entities;
using System.Threading.Tasks;

namespace MovieScrapper.Data.Interfaces
{
    public interface ITMDBRepository
    {
        Task<Movie> GetMovieAsync(string movieID);
        Task<MoviesCollection> SearchMovieAsync(string searchString);

    }
}
