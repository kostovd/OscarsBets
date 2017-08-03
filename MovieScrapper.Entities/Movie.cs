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
            this.MovieCategories = new List<MovieCategory>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        public string Overview { get; set; }

        public virtual ICollection<MovieCategory> MovieCategories { get; set; }

    }
}