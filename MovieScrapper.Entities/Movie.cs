using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MovieScrapper.Entities
{
    [JsonObject]
    [Serializable]
    public class Movie
    {
        public Movie()
        {
            this.Nominations = new List<Nomination>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ReleaseDate { get; set; }

        public string PosterPath { get; set; }

        public string Overview { get; set; }

        public string ImdbId { get; set; }

        public virtual ICollection<MovieCredit> Credits { get; set; }

        public virtual ICollection<Nomination> Nominations { get; set; }

        public virtual ICollection<Watched> UsersWatchedThisMovie { get; set; }

        public virtual ICollection<Nomination> WinningNominations { get; set; }
    }
}