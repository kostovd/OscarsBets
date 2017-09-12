using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class BetMovieObject
    {
        public string MovieTitle { get; set; }
        public string CategoryTitle { get; set; }
        public bool IsRightGuess { get; set; }
    }
}
