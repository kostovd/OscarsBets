using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScrapper.Entities
{
    [Serializable]
    public class MovieCategory
    {
        public MovieCategory()
        {
            this.Movies = new List<Movie>();
        }

        public int Id { get; set; }
        public string CategoryTtle { get; set; }
        public string CategoryDescription { get; set; }

        
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
