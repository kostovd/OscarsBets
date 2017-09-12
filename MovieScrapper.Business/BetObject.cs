using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class BetObject
    {
        public string UserEmail { get; set; }
        public List<BetMovieObject> UserBets { get; set; }
    }
}
