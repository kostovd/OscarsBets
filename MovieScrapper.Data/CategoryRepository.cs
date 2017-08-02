using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Data
{
    public class CategoryRepository
    {
        public IEnumerable<MovieCategory> GetAll()
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries.Include("Movies").ToList();
                return databaseCategory;
            }
        }

        public MovieCategory GetCategory(int id)
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries.Where(x=>x.Id==id).SingleOrDefault();
                return databaseCategory;
            }
        }

        public void AddCategory(MovieCategory category)
        {
            using (var ctx = new MovieContext())
            {                
                ctx.MovieCaterogries.Add(category);
                ctx.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {

            using (var ctx = new MovieContext())
            {
                var databaseCategory = ctx.MovieCaterogries.Where(x => x.Id == id).SingleOrDefault();
                ctx.Entry(databaseCategory).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
    }
}
