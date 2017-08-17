using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business.ViewModels
{
    public class WatchedMoviesViewModel
    {
        public Movie Movie { get; set; }
        public Watched UserId { get; set; }
    }

    
}
