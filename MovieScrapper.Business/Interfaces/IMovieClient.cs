using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business.Interfaces
{
    interface IMovieClient
    {         
        Task<Movie> GetMovieAsync(string movieID);
       
        Task<MoviesCollection> SearchMovieAsync(string searchString);       
    }
}
