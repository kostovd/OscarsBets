using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business.ViewModels
{
    public class WatchedMovies
    {
        public List<Watched> User { get; set; }
        public List<Movie> Movie { get; set; }
        
    }

    
}
