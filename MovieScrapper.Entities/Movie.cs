using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieScrapper.Entities
{
    [JsonObject]
    [Serializable]
    public class Movie
    {
        public Movie()
        {
            this.Categories = new List<Category>();
        }

        ////[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        public string Overview { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Watched> UsersWatchedThisMovie { get; set; }
        public ICollection<Bet> Bets { get; set; }
        public virtual ICollection<Category> WinningCategories { get; set; }
    }
}