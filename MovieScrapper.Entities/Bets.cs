using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MovieScrapper.Entities
{
    public class Bet
    {
        [Key]
        public string UserId { get; set; }
        public Movie Movie { get; set; }
        public Category Category { get; set; }
    }
}
