using MovieScrapper.Data;
using MovieScrapper.Entities;
using MovieScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScrapper
{
    public class MoviesLocalDBClient
    {
        public Movie[] ShowMoviesInCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").SingleOrDefault(x => x.Id == categoryId);
                var foundedMovies = databaseCategory.Movies.ToArray();

                return foundedMovies;
            }
        }

        public MovieCategory ShowCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {

                var databaseCategory = ctx.MovieCaterogries.SingleOrDefault(x => x.Id == categoryId);
                return databaseCategory;
            }
        }
    }
}