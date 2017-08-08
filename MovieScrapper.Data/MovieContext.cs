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
        public DbSet<Watched> Watched { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Movie>().Property(t => t.Id)
        //    //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        //    modelBuilder.Entity<Watched>()
        //        .HasMany(x => x.Movies)
        //        .WithMany() // <- no parameter here because there is no navigation property
        //        .Map(m =>
        //        {
        //             m.MapLeftKey("UserId");
        //            m.MapRightKey("MovieId");
        //            m.ToTable("UsersMovies");
        //        }
        //);
        //}   
    }
}