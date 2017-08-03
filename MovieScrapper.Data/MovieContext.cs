using MovieScrapper.Entities;
using System.Data.Entity;

namespace MovieScrapper.Data
{
    public class MovieContext: DbContext
    {
        public MovieContext(): base("DefaultConnection")
        {
            Database.SetInitializer<MovieContext>(new DropCreateDatabaseIfModelChanges<MovieContext>());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCategory> MovieCaterogries { get; set; }
    }
}