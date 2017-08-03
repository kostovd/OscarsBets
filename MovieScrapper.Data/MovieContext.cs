using MovieScrapper.Entities;
using System.ComponentModel.DataAnnotations.Schema;
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

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Movie>().Property(t => t.Id)
        //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        //}
    }
}