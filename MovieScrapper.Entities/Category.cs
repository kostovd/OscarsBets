using MovieScrapper.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieScrapper.Entities
{
    [Serializable]
    public class Category
    {
        public Category()
        {
            this.Nominations = new List<Nomination>();
        }

        public int Id { get; set; }
        public string CategoryTtle { get; set; }
        public string CategoryDescription { get; set; }     
        public virtual ICollection<Nomination> Nominations { get; set; }
        public ICollection<Bet> Bets { get; set; }
    }
}
