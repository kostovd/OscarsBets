using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Entities
{
    public class Watched
    {
        [Key]
        public string UserId { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

    }
}

