using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class TMDBMovieService
    {
        public async Task LoadMovieDetailsAsync(string movieId)
        {
            var environmentKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
            var repo = new TMDBRepository(environmentKey);
            var id = movieId;
            var movie = await repo.GetMovieAsync(id);           
        }
    }
}
