using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using System.Collections.Generic;

namespace MovieScrapper.Business
{
    public class CategoryService: ICategoryService
    {
        
        public void AddCategory(Category category)
        {
            var repo = new CategoryRepository();
            repo.AddCategory(category);
        }  

        public void AddMovieInCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.AddMovieInCategory(categoryId, movieId);
        }
              
        public bool AreWinnersSet()
        {
            var repo = new CategoryRepository();
            return repo.AreWinnersSet();
        }       
      
        public void DeleteCategory(int id)
        {
            var repo = new CategoryRepository();
            repo.DeleteCategory(id);
        }    

        public void EditCategory(Category category)
        {
            var repo = new CategoryRepository();
            repo.EditCategory(category);
        }    

        public IEnumerable<Category> GetAll()
        {
            var repo = new CategoryRepository();
            return repo.GetAll();
        }                                    
        
        public Category GetCategory(int id)
        {
            var repo = new CategoryRepository();
            return repo.GetCategory(id);
        }
           
        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            return repo.GetMovieInCategory(categoryId, movieId);
        }              
                         
        public void MarkAsWinner(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.MarkAsWinner(categoryId, movieId);
        }

        public void RemoveMovieFromCategory(int categoryId, int movieId)
        {
            var repo = new CategoryRepository();
            repo.RemoveMovieFromCategory(categoryId, movieId);
        }                                

    }
}
